using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bridge
{
    public partial class SaveForm: Form
    {
        public RichTextBox richTextBox;
        public DocumentManager documentManager;
        public SaveForm(RichTextBox richTextBox, DocumentManager documentManager)
        {
            this.richTextBox = richTextBox;
            this.documentManager = documentManager;

            InitializeComponent();
            SetupSizes();
        }
            
        private void SetupSizes()
        {
            this.Visible = true;
            int button_png_width = button_png.Width;
            int button_png_height = button_png.Height;

            button_png.Location = new Point(5, 5);
            button_pdf.Size = new Size(button_png_width, button_png_height);
            button_pdf.Location = new Point(10 + button_png_width, 5);

            this.ClientSize = new Size(15 + button_png_width * 2, 10 + button_png_height);
        }

        private void button_pdf_Click(object sender, EventArgs e)
        {
            documentManager.imp = new PDFDocumentSaver(richTextBox, documentManager.image);
            documentManager.SaveDocument();
            Visible = false;
        }

        private void button_png_Click(object sender, EventArgs e)
        {
            documentManager.imp = new PNGDocumentSaver(richTextBox, documentManager.image);
            richTextBox.Enabled = true;
            documentManager.SaveDocument();
            richTextBox.Enabled = false;
            Visible = false;
        }
    }
}
