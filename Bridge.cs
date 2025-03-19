//С паттерном

public class DocumentManager
{
    public DocumentSaver imp;
    public Image image;
    private RichTextBox richTextBox;
    public DocumentManager(RichTextBox richTextBox)
    {
        imp = null;
        image = null;
        this.richTextBox = richTextBox;
    }
    public bool AddImage()
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                Clipboard.SetImage(image);
                if (!string.IsNullOrWhiteSpace(richTextBox.Text))
                    richTextBox.Text += "\n";
                richTextBox.SelectionStart = richTextBox.Text.Length;
                richTextBox.ScrollToCaret();
                richTextBox.SelectionLength = 0;
                richTextBox.Paste();
                return true;
            }
        }
        return false;
    }
    public void SaveDocument()
    {
        imp.SaveDocument();
    }
}
public abstract class DocumentSaver
{
    protected Image image;
    protected RichTextBox richTextBox;
    public DocumentSaver(RichTextBox richTextBox, Image image)
    {
        this.image = image;
        this.richTextBox = richTextBox;
    }
    public abstract void SaveDocument();
}
public class PDFDocumentSaver: DocumentSaver
{
    public PDFDocumentSaver(RichTextBox richTextBox, Image image) : base(richTextBox, image) { }
    public override void SaveDocument()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
        saveFileDialog.Title = "Save as PDF";

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                if (string.IsNullOrEmpty(richTextBox.Rtf))
                {
                    return;
                }

                string rtfText = richTextBox.Rtf;

                string htmlText = Rtf.ToHtml(richTextBox.Rtf);
                Console.WriteLine(htmlText);
                htmlText = htmlText.Replace("></p>", " /></p>");
                Console.WriteLine(htmlText);

                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Png);

                document.Open();

                using (StringReader sr = new StringReader(htmlText))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                }

                int len = richTextBox.Lines.Length;

                float imageWidth = pdfImage.ScaledWidth;
                float imageHeight = pdfImage.ScaledHeight;
                float imageDelta = imageWidth / imageHeight;

                float desiredWidth = 400f;
                float desiredHeight = desiredWidth / imageDelta;

                pdfImage.ScaleToFit(desiredWidth, desiredHeight);

                float pageWidth = document.PageSize.Width;

                float x = (pageWidth - desiredWidth) / 2;

                pdfImage.SetAbsolutePosition(x, document.Top - pdfImage.ScaledHeight - 20 * len);

                document.Add(pdfImage);

                document.Close();
                
                MessageBox.Show("Файл успешно сохранён в pdf!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении PDF: {ex.Message}");
            }
        }
    }
}
public class PNGDocumentSaver: DocumentSaver
{
    public PNGDocumentSaver(RichTextBox richTextBox, Image image) : base(richTextBox, image) { }
    public override void SaveDocument()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "PNG files (*.png)|*.png";
        saveFileDialog.Title = "Save as PNG";

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            Bitmap bitmap = new Bitmap(richTextBox.Width, richTextBox.Height);

            richTextBox.DrawToBitmap(bitmap, new Rectangle(0, 0, richTextBox.Width, richTextBox.Height));

            string filePath = saveFileDialog.FileName;
            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

            MessageBox.Show($"Файл успешно сохранён в png!");
        }
    }
}