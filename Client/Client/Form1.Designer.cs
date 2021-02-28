namespace Client
{
    partial class CLIENT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CLIENT));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.portBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fileBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.uploadButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.downloadFolderBox = new System.Windows.Forms.TextBox();
            this.browseFolderButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.copyButton = new System.Windows.Forms.Button();
            this.getFileButton = new System.Windows.Forms.Button();
            this.changeAccessButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.myfileRadioButton = new System.Windows.Forms.RadioButton();
            this.publicFileRadioButton = new System.Windows.Forms.RadioButton();
            this.fileGroupBox = new System.Windows.Forms.GroupBox();
            this.fileEntityBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.fileGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 14F);
            this.label1.Location = new System.Drawing.Point(521, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "CLIENT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(13, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Server IP:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(13, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Port Number:";
            // 
            // outputBox
            // 
            this.outputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputBox.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.outputBox.Location = new System.Drawing.Point(12, 139);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(1016, 269);
            this.outputBox.TabIndex = 3;
            this.outputBox.Text = "";
            // 
            // ipBox
            // 
            this.ipBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipBox.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ipBox.Location = new System.Drawing.Point(168, 13);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(141, 30);
            this.ipBox.TabIndex = 4;
            // 
            // portBox
            // 
            this.portBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.portBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.portBox.Location = new System.Drawing.Point(168, 54);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(141, 35);
            this.portBox.TabIndex = 5;
            // 
            // connectButton
            // 
            this.connectButton.BackColor = System.Drawing.SystemColors.Info;
            this.connectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectButton.Location = new System.Drawing.Point(332, 13);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(118, 43);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "CONNECT";
            this.connectButton.UseVisualStyleBackColor = false;
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.SystemColors.Info;
            this.stopButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.Location = new System.Drawing.Point(332, 89);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(118, 41);
            this.stopButton.TabIndex = 7;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // usernameBox
            // 
            this.usernameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.usernameBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.usernameBox.Location = new System.Drawing.Point(168, 95);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(141, 35);
            this.usernameBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(13, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Username";
            // 
            // fileBox
            // 
            this.fileBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.fileBox.Location = new System.Drawing.Point(695, 29);
            this.fileBox.Name = "fileBox";
            this.fileBox.Size = new System.Drawing.Size(198, 35);
            this.fileBox.TabIndex = 11;
            // 
            // browseButton
            // 
            this.browseButton.BackColor = System.Drawing.SystemColors.Info;
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseButton.Location = new System.Drawing.Point(695, 70);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(198, 27);
            this.browseButton.TabIndex = 12;
            this.browseButton.Text = "Choose File";
            this.browseButton.UseVisualStyleBackColor = false;
            this.browseButton.Click += new System.EventHandler(this.ChooseFileButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // uploadButton
            // 
            this.uploadButton.BackColor = System.Drawing.SystemColors.Info;
            this.uploadButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uploadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uploadButton.Location = new System.Drawing.Point(908, 46);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(120, 43);
            this.uploadButton.TabIndex = 13;
            this.uploadButton.Text = "UPLOAD";
            this.uploadButton.UseVisualStyleBackColor = false;
            this.uploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // downloadFolderBox
            // 
            this.downloadFolderBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.downloadFolderBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.downloadFolderBox.Location = new System.Drawing.Point(737, 548);
            this.downloadFolderBox.Name = "downloadFolderBox";
            this.downloadFolderBox.Size = new System.Drawing.Size(291, 35);
            this.downloadFolderBox.TabIndex = 14;
            // 
            // browseFolderButton
            // 
            this.browseFolderButton.BackColor = System.Drawing.SystemColors.Info;
            this.browseFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseFolderButton.Location = new System.Drawing.Point(787, 510);
            this.browseFolderButton.Name = "browseFolderButton";
            this.browseFolderButton.Size = new System.Drawing.Size(198, 32);
            this.browseFolderButton.TabIndex = 15;
            this.browseFolderButton.Text = "Choose Download Folder";
            this.browseFolderButton.UseVisualStyleBackColor = false;
            this.browseFolderButton.Click += new System.EventHandler(this.BrowseFolderButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.BackColor = System.Drawing.SystemColors.Info;
            this.downloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadButton.Location = new System.Drawing.Point(260, 483);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(120, 43);
            this.downloadButton.TabIndex = 17;
            this.downloadButton.Text = "DOWNLOAD";
            this.downloadButton.UseVisualStyleBackColor = false;
            this.downloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.SystemColors.Info;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Location = new System.Drawing.Point(51, 484);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(120, 42);
            this.deleteButton.TabIndex = 18;
            this.deleteButton.Text = "DELETE";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // copyButton
            // 
            this.copyButton.BackColor = System.Drawing.SystemColors.Info;
            this.copyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyButton.Location = new System.Drawing.Point(469, 484);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(120, 42);
            this.copyButton.TabIndex = 19;
            this.copyButton.Text = "COPY";
            this.copyButton.UseVisualStyleBackColor = false;
            this.copyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // getFileButton
            // 
            this.getFileButton.BackColor = System.Drawing.SystemColors.Info;
            this.getFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.getFileButton.Location = new System.Drawing.Point(725, 422);
            this.getFileButton.Name = "getFileButton";
            this.getFileButton.Size = new System.Drawing.Size(198, 46);
            this.getFileButton.TabIndex = 20;
            this.getFileButton.Text = "GET FILES";
            this.getFileButton.UseVisualStyleBackColor = false;
            this.getFileButton.Click += new System.EventHandler(this.GetFileButton_Click);
            // 
            // changeAccessButton
            // 
            this.changeAccessButton.BackColor = System.Drawing.SystemColors.Info;
            this.changeAccessButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeAccessButton.Location = new System.Drawing.Point(220, 541);
            this.changeAccessButton.Name = "changeAccessButton";
            this.changeAccessButton.Size = new System.Drawing.Size(198, 42);
            this.changeAccessButton.TabIndex = 21;
            this.changeAccessButton.Text = "CHANGE ACCESS";
            this.changeAccessButton.UseVisualStyleBackColor = false;
            this.changeAccessButton.Click += new System.EventHandler(this.ChangeAccessButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Client.Properties.Resources.cloud;
            this.pictureBox1.Location = new System.Drawing.Point(473, -10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(198, 133);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // myfileRadioButton
            // 
            this.myfileRadioButton.AutoSize = true;
            this.myfileRadioButton.Location = new System.Drawing.Point(18, 8);
            this.myfileRadioButton.Name = "myfileRadioButton";
            this.myfileRadioButton.Size = new System.Drawing.Size(87, 23);
            this.myfileRadioButton.TabIndex = 23;
            this.myfileRadioButton.TabStop = true;
            this.myfileRadioButton.Text = "My Files";
            this.myfileRadioButton.UseVisualStyleBackColor = true;
            // 
            // publicFileRadioButton
            // 
            this.publicFileRadioButton.AutoSize = true;
            this.publicFileRadioButton.Location = new System.Drawing.Point(18, 31);
            this.publicFileRadioButton.Name = "publicFileRadioButton";
            this.publicFileRadioButton.Size = new System.Drawing.Size(103, 23);
            this.publicFileRadioButton.TabIndex = 24;
            this.publicFileRadioButton.TabStop = true;
            this.publicFileRadioButton.Text = "Public Files";
            this.publicFileRadioButton.UseVisualStyleBackColor = true;
            // 
            // fileGroupBox
            // 
            this.fileGroupBox.Controls.Add(this.publicFileRadioButton);
            this.fileGroupBox.Controls.Add(this.myfileRadioButton);
            this.fileGroupBox.Location = new System.Drawing.Point(920, 414);
            this.fileGroupBox.Name = "fileGroupBox";
            this.fileGroupBox.Size = new System.Drawing.Size(166, 73);
            this.fileGroupBox.TabIndex = 25;
            this.fileGroupBox.TabStop = false;
            // 
            // fileEntityBox
            // 
            this.fileEntityBox.FormattingEnabled = true;
            this.fileEntityBox.Location = new System.Drawing.Point(12, 433);
            this.fileEntityBox.Name = "fileEntityBox";
            this.fileEntityBox.Size = new System.Drawing.Size(686, 27);
            this.fileEntityBox.TabIndex = 26;
            // 
            // CLIENT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1053, 606);
            this.Controls.Add(this.getFileButton);
            this.Controls.Add(this.fileEntityBox);
            this.Controls.Add(this.fileGroupBox);
            this.Controls.Add(this.changeAccessButton);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.browseFolderButton);
            this.Controls.Add(this.downloadFolderBox);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CLIENT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLIENT";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.fileGroupBox.ResumeLayout(false);
            this.fileGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox fileBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox downloadFolderBox;
        private System.Windows.Forms.Button browseFolderButton;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Button getFileButton;
        private System.Windows.Forms.Button changeAccessButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton myfileRadioButton;
        private System.Windows.Forms.RadioButton publicFileRadioButton;
        private System.Windows.Forms.GroupBox fileGroupBox;
        private System.Windows.Forms.ComboBox fileEntityBox;
    }
}

