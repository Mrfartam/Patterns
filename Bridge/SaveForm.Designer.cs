namespace Bridge
{
    partial class SaveForm
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
            this.button_pdf = new System.Windows.Forms.Button();
            this.button_png = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_pdf
            // 
            this.button_pdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_pdf.Location = new System.Drawing.Point(42, 44);
            this.button_pdf.Name = "button_pdf";
            this.button_pdf.Size = new System.Drawing.Size(83, 41);
            this.button_pdf.TabIndex = 0;
            this.button_pdf.Text = "PDF";
            this.button_pdf.UseVisualStyleBackColor = true;
            this.button_pdf.Click += new System.EventHandler(this.button_pdf_Click);
            // 
            // button_png
            // 
            this.button_png.AutoSize = true;
            this.button_png.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_png.Location = new System.Drawing.Point(188, 44);
            this.button_png.Name = "button_png";
            this.button_png.Size = new System.Drawing.Size(83, 41);
            this.button_png.TabIndex = 1;
            this.button_png.Text = "PNG";
            this.button_png.UseVisualStyleBackColor = true;
            this.button_png.Click += new System.EventHandler(this.button_png_Click);
            // 
            // Form_save
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_png);
            this.Controls.Add(this.button_pdf);
            this.Name = "Form_save";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_save";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_pdf;
        private System.Windows.Forms.Button button_png;
    }
}