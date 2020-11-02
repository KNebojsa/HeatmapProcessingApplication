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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.labelInserted = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUploadViewCsv
            // 
            this.btnUploadViewCsv.Location = new System.Drawing.Point(12, 367);
            this.btnUploadViewCsv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUploadViewCsv.Name = "btnUploadViewCsv";
            this.btnUploadViewCsv.Size = new System.Drawing.Size(156, 28);
            this.btnUploadViewCsv.TabIndex = 13;
            this.btnUploadViewCsv.Text = "Insert Coordinates";
            this.btnUploadViewCsv.UseVisualStyleBackColor = true;
            this.btnUploadViewCsv.Click += new System.EventHandler(this.btnUploadViewsCsv_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 46);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(468, 310);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 14;
            this.pictureBox.TabStop = false;
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Location = new System.Drawing.Point(12, 12);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(158, 28);
            this.btnUploadImage.TabIndex = 15;
            this.btnUploadImage.Text = "Upload Image";
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(350, 405);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(130, 37);
            this.btnProcess.TabIndex = 16;
            this.btnProcess.Text = "Process image";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // labelInserted
            // 
            this.labelInserted.AutoSize = true;
            this.labelInserted.Location = new System.Drawing.Point(201, 374);
            this.labelInserted.Name = "labelInserted";
            this.labelInserted.Size = new System.Drawing.Size(69, 21);
            this.labelInserted.TabIndex = 17;
            this.labelInserted.Text = ".csv file";
            // 
            // HeatmapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 468);
            this.Controls.Add(this.labelInserted);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.btnUploadViewCsv);
            this.Controls.Add(this.pictureBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "HeatmapViewer";
            this.Text = "AppProccessingApp";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnUploadViewCsv;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label labelInserted;
    }
}

