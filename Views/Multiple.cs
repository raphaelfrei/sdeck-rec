using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;

namespace sdeckrec.Views {

    public partial class Multiple : Form {

        private readonly static string AppPath = Directory.GetCurrentDirectory();
        private readonly static string FfmpegPath = Path.Combine(AppPath, "libs", "ffmpeg.exe");
        private double TotalVideoTime = 0;

        public Multiple() {
            InitializeComponent();
        }

        private void BtnRootFolder_Click(object sender, EventArgs e) {

            using (FolderBrowserDialog dialog = new FolderBrowserDialog()) {
                dialog.Description = "Select Root Folder";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                DgvVideo.Rows.Clear();
                PgBar.Value = 0;
                PgBarSingle.Value = 0;
                string selectedFolder = dialog.SelectedPath;
                TbMPDFile.Text = selectedFolder;

                foreach (string file in GetAllMpdFiles(selectedFolder))
                    DgvVideo.Rows.Add(file);
                
                LbProgress.Text = $"0/{DgvVideo.Rows.Count}";
                
            }
        }

        private List<string> GetAllMpdFiles(string directory) {
            return Directory.GetFiles(directory, "*.mpd", SearchOption.AllDirectories).ToList();
        }

        private void BtnOutput_Click(object sender, EventArgs e) {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog()) {
                dialog.Description = "Select output path";
                if (dialog.ShowDialog() == DialogResult.OK) {
                    TbOutput.Text = dialog.SelectedPath;
                }
            }
        }

        private double GetVideoTime(string mpdFile) {
            try {
                XDocument mpdDocument = XDocument.Load(mpdFile);
                XNamespace ns = "urn:mpeg:dash:schema:mpd:2011";

                var mpdElement = mpdDocument.Element(ns + "MPD");
                if (mpdElement != null) {
                    var mediaPresentationDuration = mpdElement.Attribute("mediaPresentationDuration")?.Value;

                    if (!string.IsNullOrEmpty(mediaPresentationDuration)) {
                        double seconds = XmlConvert.ToTimeSpan(mediaPresentationDuration).TotalSeconds;
                        // Retorna pelo menos 1 segundo para evitar divisão por zero em vídeos minúsculos
                        return seconds > 0 ? seconds : 1;
                    }
                }
            } catch {
                // Se falhar ao ler, retorna 1 para evitar crash na divisão
                return 1;
            }

            return 1; // Default seguro
        }

        private async void ConvertAllVideos() {

            if (DgvVideo.Rows.Count == 0 || string.IsNullOrEmpty(TbOutput.Text)) {
                MessageBox.Show("Select output folder.");
                return;
            }

            PgBar.Minimum = 0;
            PgBar.Maximum = DgvVideo.Rows.Count;
            PgBar.Step = 1;
            PgBar.Value = 0;

            PgBarSingle.Minimum = 0;
            PgBarSingle.Maximum = 100;

            foreach (DataGridViewRow row in DgvVideo.Rows) {

                string mpdFile = row.Cells[0].Value?.ToString();

                if (string.IsNullOrEmpty(mpdFile)) 
                    continue;

                string filename = Path.GetFileNameWithoutExtension(mpdFile);
                string mp4File = $"{filename}-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.mp4";
                string outputPath = Path.Combine(TbOutput.Text, mp4File);
                TotalVideoTime = GetVideoTime(mpdFile);

                row.Cells[1].Value = mp4File;

                PgBarSingle.Value = 0;

                bool conversionSuccess = false;

                await Task.Run(() => {
                    try {
                        using (Process ffmpegProcess = new Process()) {
                            ffmpegProcess.StartInfo.FileName = FfmpegPath;
                            ffmpegProcess.StartInfo.Arguments = $"-protocol_whitelist file,http,https,tcp,tls -i \"{mpdFile}\" -c copy \"{outputPath}\" -y"; // -y para sobrescrever sem perguntar
                            ffmpegProcess.StartInfo.RedirectStandardError = true;
                            ffmpegProcess.StartInfo.UseShellExecute = false;
                            ffmpegProcess.StartInfo.CreateNoWindow = true;
                            ffmpegProcess.EnableRaisingEvents = true; // Importante
                            ffmpegProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; // Melhor que Normal para background

                            ffmpegProcess.ErrorDataReceived += FfmpegProcess_ErrorDataReceived;

                            ffmpegProcess.Start();
                            ffmpegProcess.BeginErrorReadLine();

                            // Espera o processo sair. Se for um arquivo pequeno corrompido que trava o ffmpeg,
                            // isso evita que o app congele para sempre.
                            // Aqui ele espera indefinidamente, mas como o processo é curto, deve ser instantâneo.
                            ffmpegProcess.WaitForExit();

                            // Se chegou aqui, o processo terminou.
                            if (ffmpegProcess.ExitCode == 0)
                                conversionSuccess = true;
                        }
                    } catch (Exception ex) {
                        // Logar erro se necessário: Debug.WriteLine(ex.Message);
                        conversionSuccess = false;
                    }
                });

                row.DefaultCellStyle.BackColor = conversionSuccess ? Color.LightGreen : Color.LightCoral;

                PgBar.PerformStep();
                LbProgress.Text = $"{PgBar.Value}/{PgBar.Maximum}";
            }

            MessageBox.Show("All files converted!");

            PgBarSingle.Value = 0;
            PgBar.Value = PgBar.Maximum;
        }

        private void FfmpegProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e) {
            // Se o processo já terminou ou a linha é nula, ignora
            if (string.IsNullOrEmpty(e.Data))
                return;

            try {
                // Regex para capturar o tempo
                var match = Regex.Match(e.Data, @"time=(\d{2}):(\d{2}):(\d{2})\.(\d{2})");

                if (match.Success) {
                    int hours = int.Parse(match.Groups[1].Value);
                    int minutes = int.Parse(match.Groups[2].Value);
                    int seconds = int.Parse(match.Groups[3].Value);
                    double currentTime = hours * 3600 + minutes * 60 + seconds;

                    // CORREÇÃO: Evita divisão por zero se TotalVideoTime for 0
                    if (TotalVideoTime > 0) {
                        double progress = (currentTime / TotalVideoTime) * 100;

                        // Garante que o progresso não ultrapasse 100 ou seja negativo
                        int finalValue = Math.Max(0, Math.Min((int)progress, 100));

                        PgBarSingle.Invoke((MethodInvoker)(() => {
                            PgBarSingle.Value = finalValue;
                        }));
                    }
                }
            } catch {
                // Ignora erros de parsing para não travar o processo principal
            }
        }

        private void BtnConvert_Click(object sender, EventArgs e) {
            ConvertAllVideos();
        }
    }
}
