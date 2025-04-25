namespace sdeckrec.Views {
    partial class Multiple {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Multiple));
            label3 = new Label();
            BtnRootFolder = new Button();
            TbMPDFile = new TextBox();
            label2 = new Label();
            label8 = new Label();
            BtnOutput = new Button();
            TbOutput = new TextBox();
            label9 = new Label();
            DgvVideo = new DataGridView();
            Attribute = new DataGridViewTextBoxColumn();
            ExportedFile = new DataGridViewTextBoxColumn();
            label4 = new Label();
            PgBar = new ProgressBar();
            BtnConvert = new Button();
            PgBarSingle = new ProgressBar();
            LbProgress = new Label();
            label1 = new Label();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)DgvVideo).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 98);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 9;
            label3.Text = "Root Directory:";
            // 
            // BtnRootFolder
            // 
            BtnRootFolder.Location = new Point(666, 94);
            BtnRootFolder.Name = "BtnRootFolder";
            BtnRootFolder.Size = new Size(111, 23);
            BtnRootFolder.TabIndex = 8;
            BtnRootFolder.Text = "&Search";
            BtnRootFolder.UseVisualStyleBackColor = true;
            BtnRootFolder.Click += BtnRootFolder_Click;
            // 
            // TbMPDFile
            // 
            TbMPDFile.Location = new Point(113, 94);
            TbMPDFile.Name = "TbMPDFile";
            TbMPDFile.ReadOnly = true;
            TbMPDFile.Size = new Size(547, 23);
            TbMPDFile.TabIndex = 6;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.Fixed3D;
            label2.Location = new Point(9, 88);
            label2.Name = "label2";
            label2.Size = new Size(776, 35);
            label2.TabIndex = 7;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(21, 130);
            label8.Name = "label8";
            label8.Size = new Size(84, 15);
            label8.TabIndex = 20;
            label8.Text = "Output Folder:";
            // 
            // BtnOutput
            // 
            BtnOutput.Location = new Point(666, 126);
            BtnOutput.Name = "BtnOutput";
            BtnOutput.Size = new Size(111, 23);
            BtnOutput.TabIndex = 19;
            BtnOutput.Text = "&Search";
            BtnOutput.UseVisualStyleBackColor = true;
            BtnOutput.Click += BtnOutput_Click;
            // 
            // TbOutput
            // 
            TbOutput.Location = new Point(111, 126);
            TbOutput.Name = "TbOutput";
            TbOutput.ReadOnly = true;
            TbOutput.Size = new Size(549, 23);
            TbOutput.TabIndex = 17;
            // 
            // label9
            // 
            label9.BorderStyle = BorderStyle.Fixed3D;
            label9.Location = new Point(9, 120);
            label9.Name = "label9";
            label9.Size = new Size(776, 35);
            label9.TabIndex = 18;
            // 
            // DgvVideo
            // 
            DgvVideo.AllowUserToAddRows = false;
            DgvVideo.AllowUserToDeleteRows = false;
            DgvVideo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DgvVideo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvVideo.Columns.AddRange(new DataGridViewColumn[] { Attribute, ExportedFile });
            DgvVideo.Location = new Point(21, 180);
            DgvVideo.MultiSelect = false;
            DgvVideo.Name = "DgvVideo";
            DgvVideo.ReadOnly = true;
            DgvVideo.RowHeadersVisible = false;
            DgvVideo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvVideo.Size = new Size(756, 155);
            DgvVideo.TabIndex = 22;
            // 
            // Attribute
            // 
            Attribute.HeaderText = "Original File";
            Attribute.Name = "Attribute";
            Attribute.ReadOnly = true;
            // 
            // ExportedFile
            // 
            ExportedFile.HeaderText = "Exported File";
            ExportedFile.Name = "ExportedFile";
            ExportedFile.ReadOnly = true;
            // 
            // label4
            // 
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.Location = new Point(9, 155);
            label4.Name = "label4";
            label4.Size = new Size(776, 194);
            label4.TabIndex = 21;
            // 
            // PgBar
            // 
            PgBar.Location = new Point(9, 381);
            PgBar.Name = "PgBar";
            PgBar.Size = new Size(776, 23);
            PgBar.TabIndex = 24;
            // 
            // BtnConvert
            // 
            BtnConvert.Location = new Point(9, 410);
            BtnConvert.Name = "BtnConvert";
            BtnConvert.Size = new Size(776, 23);
            BtnConvert.TabIndex = 25;
            BtnConvert.Text = "&Convert";
            BtnConvert.UseVisualStyleBackColor = true;
            BtnConvert.Click += BtnConvert_Click;
            // 
            // PgBarSingle
            // 
            PgBarSingle.Location = new Point(9, 352);
            PgBarSingle.Name = "PgBarSingle";
            PgBarSingle.Size = new Size(776, 23);
            PgBarSingle.TabIndex = 26;
            // 
            // LbProgress
            // 
            LbProgress.Location = new Point(666, 162);
            LbProgress.Name = "LbProgress";
            LbProgress.Size = new Size(111, 15);
            LbProgress.TabIndex = 27;
            LbProgress.Text = "0/0";
            LbProgress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 26F);
            label1.ImageAlign = ContentAlignment.BottomCenter;
            label1.Location = new Point(9, 9);
            label1.Name = "label1";
            label1.Size = new Size(776, 67);
            label1.TabIndex = 28;
            label1.Text = "Steam Deck Recording - MP4 Generator";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 436);
            label7.Name = "label7";
            label7.Size = new Size(105, 15);
            label7.TabIndex = 29;
            label7.Text = "sdeckrec.cs [v1.00]";
            // 
            // Multiple
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 457);
            Controls.Add(label7);
            Controls.Add(label1);
            Controls.Add(LbProgress);
            Controls.Add(PgBarSingle);
            Controls.Add(BtnConvert);
            Controls.Add(PgBar);
            Controls.Add(DgvVideo);
            Controls.Add(label4);
            Controls.Add(label8);
            Controls.Add(BtnOutput);
            Controls.Add(TbOutput);
            Controls.Add(label9);
            Controls.Add(label3);
            Controls.Add(BtnRootFolder);
            Controls.Add(TbMPDFile);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Multiple";
            Text = "Steam Deck - Recoding Convert © 2025 - Raphael Frei";
            ((System.ComponentModel.ISupportInitialize)DgvVideo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Button BtnRootFolder;
        private TextBox TbMPDFile;
        private Label label2;
        private Label label8;
        private Button BtnOutput;
        private TextBox TbOutput;
        private Label label9;
        private DataGridView DgvVideo;
        private Label label4;
        private ProgressBar PgBar;
        private Button BtnConvert;
        private ProgressBar PgBarSingle;
        private Label LbProgress;
        private Label label1;
        private Label label7;
        private DataGridViewTextBoxColumn Attribute;
        private DataGridViewTextBoxColumn ExportedFile;
    }
}