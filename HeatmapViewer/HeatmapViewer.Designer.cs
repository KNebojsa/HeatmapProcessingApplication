namespace WfaClient
{
    partial class HeatmapViewer
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
            this.btnUploadViewCsv = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUploadViewCsv
            // 
            this.btnUploadViewCsv.Location = new System.Drawing.Point(12, 12);
            this.btnUploadViewCsv.Name = "btnUploadViewCsv";
            this.btnUploadViewCsv.Size = new System.Drawing.Size(117, 23);
            this.btnUploadViewCsv.TabIndex = 13;
            this.btnUploadViewCsv.Text = "Upload";
            this.btnUploadViewCsv.UseVisualStyleBackColor = true;
            this.btnUploadViewCsv.Click += new System.EventHandler(this.btnUploadViewsCsv_Click);
            // 
            // HeatmapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 380);
            this.Controls.Add(this.btnUploadViewCsv);
            this.Name = "HeatmapViewer";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnUploadViewCsv;
    }
}

