namespace AmazonEncoder
{
    partial class AmazonEncoder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AmazonEncoder));
            this.btn_EncodeFiles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_DeleteOriginal = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_EncodeFiles
            // 
            this.btn_EncodeFiles.Location = new System.Drawing.Point(14, 14);
            this.btn_EncodeFiles.Name = "btn_EncodeFiles";
            this.btn_EncodeFiles.Size = new System.Drawing.Size(303, 27);
            this.btn_EncodeFiles.TabIndex = 0;
            this.btn_EncodeFiles.Text = "Encode Files";
            this.btn_EncodeFiles.UseVisualStyleBackColor = true;
            this.btn_EncodeFiles.Click += new System.EventHandler(this.btn_EncodeFiles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Progress:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 77);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(307, 23);
            this.progressBar.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(307, 21);
            this.label2.TabIndex = 3;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk_DeleteOriginal
            // 
            this.chk_DeleteOriginal.AutoSize = true;
            this.chk_DeleteOriginal.Location = new System.Drawing.Point(194, 47);
            this.chk_DeleteOriginal.Name = "chk_DeleteOriginal";
            this.chk_DeleteOriginal.Size = new System.Drawing.Size(125, 19);
            this.chk_DeleteOriginal.TabIndex = 4;
            this.chk_DeleteOriginal.Text = "Delete Original File";
            this.chk_DeleteOriginal.UseVisualStyleBackColor = true;
            // 
            // AmazonEncoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 133);
            this.Controls.Add(this.chk_DeleteOriginal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_EncodeFiles);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AmazonEncoder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Amazon Drive Encoder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_EncodeFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chk_DeleteOriginal;
    }
}

