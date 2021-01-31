namespace FixHistories
{
    partial class frmLoadStatus
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
            this.btnfrmLoadStausCancel = new System.Windows.Forms.Button();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.tbLoadStatus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnfrmLoadStausCancel
            // 
            this.btnfrmLoadStausCancel.Location = new System.Drawing.Point(207, 82);
            this.btnfrmLoadStausCancel.Name = "btnfrmLoadStausCancel";
            this.btnfrmLoadStausCancel.Size = new System.Drawing.Size(75, 23);
            this.btnfrmLoadStausCancel.TabIndex = 7;
            this.btnfrmLoadStausCancel.Text = "Cancel";
            this.btnfrmLoadStausCancel.UseVisualStyleBackColor = true;
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(58, 53);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(394, 23);
            this.pbStatus.TabIndex = 6;
            // 
            // tbLoadStatus
            // 
            this.tbLoadStatus.Location = new System.Drawing.Point(58, 12);
            this.tbLoadStatus.Name = "tbLoadStatus";
            this.tbLoadStatus.Size = new System.Drawing.Size(394, 20);
            this.tbLoadStatus.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Status: ";
            // 
            // frmLoadStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 123);
            this.Controls.Add(this.btnfrmLoadStausCancel);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.tbLoadStatus);
            this.Controls.Add(this.label1);
            this.Name = "frmLoadStatus";
            this.Text = "frmLoadStatus";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnfrmLoadStausCancel;
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.TextBox tbLoadStatus;
        private System.Windows.Forms.Label label1;
    }
}