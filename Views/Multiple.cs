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

            XDocument mpdDocument = XDocument.Load(mpdFile);
            XNamespace ns = "urn:mpeg:dash:schema:mpd:2011";

            var mpdElement = mpdDocument.Element(ns + "MPD");
            if (mpdElement != null) {
                var mediaPresentationDuration = mpdElement.Attribute("mediaPresentationDuration")?.Value;

                return XmlConvert.ToTimeSpan(mediaPresentationDuration).TotalSeconds;
                
            }

            return 0;
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
                        Process ffmpegProcess = new Process();
                        ffmpegProcess.StartInfo.FileName = FfmpegPath;
                        ffmpegProcess.StartInfo.Arguments = $"-protocol_whitelist file,http,https,tcp,tls -i \"{mpdFile}\" -c copy \"{outputPath}\"";
                        ffmpegProcess.StartInfo.RedirectStandardError = true;
                        ffmpegProcess.StartInfo.UseShellExecute = false;
                        ffmpegProcess.StartInfo.CreateNoWindow = true;
                        ffmpegProcess.EnableRaisingEvents = true;
                        ffmpegProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        ffmpegProcess.ErrorDataReceived += FfmpegProcess_ErrorDataReceived;
                        ffmpegProcess.Start();
                        ffmpegProcess.BeginErrorReadLine();
                        ffmpegProcess.WaitForExit();

                        if (ffmpegProcess.ExitCode == 0)
                            conversionSuccess = true;
                    } catch {
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

            if (string.IsNullOrEmpty(e.Data))
                return;

            // Regex to capture current video time (Example: time=00:33:09.26)
            var match = Regex.Match(e.Data, @"time=(\d{2}):(\d{2}):(\d{2})\.(\d{2})");

            if (match.Success) {

                int hours = int.Parse(match.Groups[1].Value);
                int minutes = int.Parse(match.Groups[2].Value);
                int seconds = int.Parse(match.Groups[3].Value);
                double currentTime = hours * 3600 + minutes * 60 + seconds;

                // Get percentage comparing to TotalVideoTime
                double progress = (currentTime / TotalVideoTime) * 100;

                PgBarSingle.Invoke((MethodInvoker)(() => {
                    PgBarSingle.Value = Math.Min((int)progress, PgBarSingle.Maximum);
                }));
            }
        }

        private void BtnConvert_Click(object sender, EventArgs e) {
            ConvertAllVideos();
        }
    }
}
