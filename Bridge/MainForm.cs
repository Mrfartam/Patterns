using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bridge
{
    public partial class MainForm : Form
    {
        public DocumentManager documentManager;
        public MainForm()
        {
            InitializeComponent();
            SetupSizes();
        }

        private void SetupSizes()
        {
            this.ClientSize = new Size(600, Math.Max(button_save.Location.Y + button_save.Height + 5, 600));
            int form_width = this.ClientSize.Width;
            int form_height = this.ClientSize.Height;

            int label_width = label1.Width;
            int label_height = label1.Height;
            label1.Location = new Point((form_width - label_width) / 2, 5);

            richTextBox.Size = new Size(700, richTextBox.Height);
            int text_box_width = richTextBox.Width;
            int text_box_height = richTextBox.Height;
            richTextBox.Location = new Point((form_width - text_box_width) / 2, 10 + label_height);

            int button_add_img_width = button_add_image.Width;
            int button_add_img_height = button_add_image.Height;
            button_add_image.Location = new Point((form_width - button_add_img_width) / 2, 15 + label_height + text_box_height);

            int button_save_width = button_save.Width;
            int button_save_height = button_save.Height;
            button_save.Location = new Point((form_width - button_save_width) / 2, 20 + label_height + text_box_height + button_add_img_height);
            ClientSize = new Size(600, Math.Max(button_save.Location.Y + button_save.Height + 5, 600));
        }
        private void AutoSize_RichTextBox(object sender, ContentsResizedEventArgs e)
        {
            int delta_height = e.NewRectangle.Height - richTextBox.Height;
            int delta_width = e.NewRectangle.Width - richTextBox.Width;

            richTextBox.Height = e.NewRectangle.Height + 10;
            SetupSizes();
        }

        private void button_add_image_Click(object sender, EventArgs e)
        {
            documentManager = new DocumentManager(richTextBox);
            if (documentManager.AddImage())
            {
                richTextBox.Enabled = false;
                label1.Text = "Сохраните документ";
            }
            
            SetupSizes();
        }
        private void button_save_Click(object sender, EventArgs e)
        {
            if (documentManager == null) documentManager = new DocumentManager(richTextBox);
            SaveForm new_form = new SaveForm(richTextBox, documentManager);
            new_form.Visible = false;
            new_form.ShowDialog();
        }
    }
}
