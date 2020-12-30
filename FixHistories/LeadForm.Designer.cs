namespace FixHistories
{
    partial class LeadForm
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
            this.lbFileList = new System.Windows.Forms.ListBox();
            this.tbOutputFileName = new System.Windows.Forms.TextBox();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.tbDirectory = new System.Windows.Forms.TextBox();
            this.btnConCatSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.btnSetStartToMaxRecs = new System.Windows.Forms.Button();
            this.btnCleanSequences = new System.Windows.Forms.Button();
            this.btnDeDuplicate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbFileList
            // 
            this.lbFileList.FormattingEnabled = true;
            this.lbFileList.Location = new System.Drawing.Point(437, 35);
            this.lbFileList.Margin = new System.Windows.Forms.Padding(2);
            this.lbFileList.Name = "lbFileList";
            this.lbFileList.Size = new System.Drawing.Size(277, 212);
            this.lbFileList.TabIndex = 0;
            // 
            // tbOutputFileName
            // 
            this.tbOutputFileName.Location = new System.Drawing.Point(187, 174);
            this.tbOutputFileName.Margin = new System.Windows.Forms.Padding(2);
            this.tbOutputFileName.Name = "tbOutputFileName";
            this.tbOutputFileName.Size = new System.Drawing.Size(246, 20);
            this.tbOutputFileName.TabIndex = 1;
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.Location = new System.Drawing.Point(19, 16);
            this.btnSelectFiles.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(150, 42);
            this.btnSelectFiles.TabIndex = 2;
            this.btnSelectFiles.Text = "Select Files";
            this.btnSelectFiles.UseVisualStyleBackColor = true;
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);
            // 
            // tbDirectory
            // 
            this.tbDirectory.Location = new System.Drawing.Point(437, 16);
            this.tbDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.tbDirectory.Name = "tbDirectory";
            this.tbDirectory.Size = new System.Drawing.Size(209, 20);
            this.tbDirectory.TabIndex = 3;
            this.tbDirectory.Text = "C:\\Users\\greig\\Dropbox\\J3\\NRCS\\Rational";
            // 
            // btnConCatSave
            // 
            this.btnConCatSave.Location = new System.Drawing.Point(19, 159);
            this.btnConCatSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnConCatSave.Name = "btnConCatSave";
            this.btnConCatSave.Size = new System.Drawing.Size(150, 36);
            this.btnConCatSave.TabIndex = 4;
            this.btnConCatSave.Text = "Select && Save History File";
            this.btnConCatSave.UseVisualStyleBackColor = true;
            this.btnConCatSave.Click += new System.EventHandler(this.btnConCatSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "lblStatus";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(190, 78);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 6;
            this.dtpStartDate.Value = new System.DateTime(2000, 1, 1, 15, 13, 0, 0);
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(190, 132);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 7;
            this.dtpEndDate.Value = new System.DateTime(3020, 1, 1, 15, 13, 0, 0);
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(190, 59);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblStartDate.TabIndex = 8;
            this.lblStartDate.Text = "Start Date";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(193, 113);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(52, 13);
            this.lblEndDate.TabIndex = 9;
            this.lblEndDate.Text = "End Date";
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(19, 84);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(150, 39);
            this.btnLoadData.TabIndex = 10;
            this.btnLoadData.Text = "Load && Merge Files";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Location = new System.Drawing.Point(190, 159);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(144, 13);
            this.lblNumberOfRecords.TabIndex = 11;
            this.lblNumberOfRecords.Text = "Number of Records 0000000";
            // 
            // btnSetStartToMaxRecs
            // 
            this.btnSetStartToMaxRecs.Location = new System.Drawing.Point(190, 16);
            this.btnSetStartToMaxRecs.Name = "btnSetStartToMaxRecs";
            this.btnSetStartToMaxRecs.Size = new System.Drawing.Size(199, 31);
            this.btnSetStartToMaxRecs.TabIndex = 12;
            this.btnSetStartToMaxRecs.Text = "Set Start Date to Max Records";
            this.btnSetStartToMaxRecs.UseVisualStyleBackColor = true;
            this.btnSetStartToMaxRecs.Click += new System.EventHandler(this.btnSetStartToMaxRecs_Click);
            // 
            // btnCleanSequences
            // 
            this.btnCleanSequences.Location = new System.Drawing.Point(22, 210);
            this.btnCleanSequences.Margin = new System.Windows.Forms.Padding(2);
            this.btnCleanSequences.Name = "btnCleanSequences";
            this.btnCleanSequences.Size = new System.Drawing.Size(150, 36);
            this.btnCleanSequences.TabIndex = 13;
            this.btnCleanSequences.Text = "Clean Sequences";
            this.btnCleanSequences.UseVisualStyleBackColor = true;
            this.btnCleanSequences.Click += new System.EventHandler(this.btnCleanSequences_Click);
            // 
            // btnDeDuplicate
            // 
            this.btnDeDuplicate.Location = new System.Drawing.Point(190, 210);
            this.btnDeDuplicate.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeDuplicate.Name = "btnDeDuplicate";
            this.btnDeDuplicate.Size = new System.Drawing.Size(150, 36);
            this.btnDeDuplicate.TabIndex = 14;
            this.btnDeDuplicate.Text = "De-Duplicate";
            this.btnDeDuplicate.UseVisualStyleBackColor = true;
            this.btnDeDuplicate.Click += new System.EventHandler(this.btnDeDuplicate_Click);
            // 
            // LeadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 257);
            this.Controls.Add(this.btnDeDuplicate);
            this.Controls.Add(this.btnCleanSequences);
            this.Controls.Add(this.btnSetStartToMaxRecs);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConCatSave);
            this.Controls.Add(this.tbDirectory);
            this.Controls.Add(this.btnSelectFiles);
            this.Controls.Add(this.tbOutputFileName);
            this.Controls.Add(this.lbFileList);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LeadForm";
            this.Text = "FixHistories";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbFileList;
        private System.Windows.Forms.TextBox tbOutputFileName;
        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.TextBox tbDirectory;
        private System.Windows.Forms.Button btnConCatSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Button btnSetStartToMaxRecs;
        private System.Windows.Forms.Button btnCleanSequences;
        private System.Windows.Forms.Button btnDeDuplicate;
    }
}

