namespace Bridge
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button_add_image = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(193, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите текст:";
            // 
            // button_add_image
            // 
            this.button_add_image.AutoSize = true;
            this.button_add_image.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.button_add_image.Location = new System.Drawing.Point(233, 245);
            this.button_add_image.Name = "button_add_image";
            this.button_add_image.Size = new System.Drawing.Size(266, 41);
            this.button_add_image.TabIndex = 0;
            this.button_add_image.Text = "Добавить картинку";
            this.button_add_image.Click += new System.EventHandler(this.button_add_image_Click);
            // 
            // button_save
            // 
            this.button_save.AutoSize = true;
            this.button_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.button_save.Location = new System.Drawing.Point(290, 320);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(160, 41);
            this.button_save.TabIndex = 2;
            this.button_save.Text = "Сохранить";
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox.Location = new System.Drawing.Point(136, 135);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(500, 64);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = "";
            this.richTextBox.ContentsResized += AutoSize_RichTextBox;
            this.richTextBox.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_add_image);
            this.Controls.Add(this.button_save);
            this.Name = "Form1";
            this.Text = "Создание и сохранение текста с фото";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button button_add_image;
        public System.Windows.Forms.Button button_save;
        public System.Windows.Forms.RichTextBox richTextBox;
    }
}

