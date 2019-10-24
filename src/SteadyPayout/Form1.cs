using BusinessLayer;
using DatabaseLayer;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SendGrid;
using SendGrid.Helpers.Mail;
using SteadyPayout;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        OcrTemplates Templates = new OcrTemplates();
        Employees employees = new Employees();

        Rectangle? rec = new Rectangle();
        List<OcrImage> images = new List<OcrImage>();
        List<Rectangle> recs = new List<Rectangle>();
        readonly List<Rectangle> identificationRecs = new List<Rectangle>();

        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateStatusBar(string msg)
        {
            toolStripStatusLabel2.Text = msg;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadImages();
            recs.Clear();
            identificationRecs.Clear();
            saveToolStripMenuItem.Enabled = (identificationRecs.Count > 0 && recs.Count > 1);
        }

        private void LoadImages()
        {

            UpdateStatusBar("Loading Images");
            images.Clear();
            pictureBox1.Width = 0;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            foreach (string f in Directory.GetFiles(ConfigurationManager.AppSettings.Get("ApplicationDirectory"), "*.pdf"))
            {
                UpdateStatusBar("Loading Image : " + f);
                images.Add(new OcrImage(f, new MySmtp()));
            }

            if (images.Any())
                pictureBox1.Image = images[0].GetImage();

            toolStripStatusLabel1.Text = "Total Pages : " + images.Count;
            processDocuments.Enabled = (this.images.Any());
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "X=" + e.Location.X.ToString() + " Y=" + e.Location.Y.ToString();
            if ((identificationRecs.Count == 1 && e.Button == MouseButtons.Right) || (recs.Count == 2 && e.Button == MouseButtons.Left))
            {
                MessageBox.Show("You are only allowed 1 Purple and 2 Red boxes");
            }
            else
            {
                rec = new Rectangle(e.Location.X, e.Location.Y, 100, 100);
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (rec == null)
                return;
            if (e.Button == MouseButtons.Right)
            {
                identificationRecs.Add(rec.Value);

                var data = images[0].IdentifyData(new Rectangle[] { rec.Value });
                foreach (string s in data)
                {
                    UpdateStatusBar($"Field Identified as : { s}");
                }
            }
            else
            {
                recs.Add(rec.Value);
            }
            rec = null;
            DrawRectangles(recs, Color.Red, "Data");
            DrawIdentifications(identificationRecs, Color.Purple, "Identification");
            saveToolStripMenuItem.Enabled = (identificationRecs.Count > 0 && recs.Count > 1);
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (rec == null)
                return;

            if (e.Button == MouseButtons.Left)
            {
                rec = new Rectangle(rec.Value.X, rec.Value.Y, e.Location.X - rec.Value.X, e.Location.Y - rec.Value.Y);
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    pictureBox1.CreateGraphics().DrawRectangle(pen, rec.Value);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                rec = new Rectangle(rec.Value.X, rec.Value.Y, e.Location.X - rec.Value.X, e.Location.Y - rec.Value.Y);
                using (Pen pen = new Pen(Color.Purple, 2))
                {
                    pictureBox1.CreateGraphics().DrawRectangle(pen, rec.Value);
                }
            }
        }

        private void GetIdentificationFields(IEnumerable<string> data, out string name, out string code)
        {
            try
            {
                name = data.ToList()[0];
                code = data.ToList()[1];
            }
            catch (Exception)
            {
                name = "";
                code = "";
            }
            finally
            {

            }
        }

        private OcrTemplate FindTemplate(OcrImage ocrImage)
        {
            foreach (var t in Templates.GetTemplates())
            {
                var idFields = t.IdentificationData.ToRecs();
                if (ocrImage.IdentifyData(idFields).ToList()[0].ToLower() == t.TemplateDescription.Replace("Template", "").ToLower())
                {
                    return t;
                }
            }
            throw new Exception("Unable to find a template that match the document");
        }

        private void DrawIdentifications(IEnumerable<Rectangle> rectangles, Color penColor, string wording)
        {
            foreach (var r in rectangles)
            {
                using (Pen pen = new Pen(penColor, 2))
                {

                    var gfx = pictureBox1.CreateGraphics();
                    gfx.DrawRectangle(pen, r);
                    if (wording == "Data")
                    {
                        gfx.DrawString(wording + " Field " + (rectangles.ToList().IndexOf(r) + 1), new Font("Arial", 10), new SolidBrush(Color.Black), r.Left, r.Top - 15);
                    }
                    else
                    {
                        gfx.DrawString(wording + " Field", new Font("Arial", 10), new SolidBrush(Color.Black), r.Left, r.Top - 15);
                    }

                }
            }
        }

        private void DrawRectangles(IEnumerable<Rectangle> rectangles, Color penColor, string wording)
        {
            pictureBox1.Refresh();
            DrawIdentifications(rectangles, penColor, wording);
        }

        private void ExtractPages(string sourcePDFpath, string outputPDFpath, int startpage, int endpage)
        {
            PdfReader reader = null;
            iTextSharp.text.Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage = null;

            PdfReader.unethicalreading = true;
            reader = new PdfReader(sourcePDFpath);


            sourceDocument = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(startpage));
            pdfCopyProvider = new PdfCopy(sourceDocument, new FileStream(outputPDFpath, FileMode.Create));
            sourceDocument.Open();


            for (int i = startpage; i < startpage + endpage; i++)
            {
                Application.DoEvents();
                try
                {
                    importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                    pdfCopyProvider.AddPage(importedPage);
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
            }
            sourceDocument.Close();
            reader.Close();
        }

        private void UploadPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatusBar("Uploading PDF");
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Portable Document|*.pdf";
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                var d = dialog.FileName;

                var reader = new PdfReader(d);
                int nop = reader.NumberOfPages;
                reader.Dispose();
                for (int i = 1; i <= nop; i++)
                {
                    UpdateStatusBar(String.Format("Extracting page {0} of {1}", i, nop));
                    ExtractPages(d, Path.Combine(ConfigurationManager.AppSettings.Get("ApplicationDirectory"), Guid.NewGuid().ToString()) + ".pdf", i, 1);
                }
                LoadImages();
            }
        }

        private void ResetIdentificationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            recs.Clear();
            identificationRecs.Clear();
            pictureBox1.Refresh();
        }


        private void TemplateNew_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Portable Document|*.pdf";
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                var d = dialog.FileName;

                var reader = new PdfReader(d);
                int nop = reader.NumberOfPages;
                reader.Dispose();
                for (int i = 1; i <= nop; i++)
                {
                    UpdateStatusBar(String.Format("Extracting page {0} of {1}", i, nop));
                    ExtractPages(d, Path.Combine(ConfigurationManager.AppSettings.Get("ApplicationDirectory"), Guid.NewGuid().ToString()) + ".pdf", i, 1);
                }
                LoadImages();
            }
        }

        private void TemplateSave_Click(object sender, EventArgs e)
        {
            if (recs.Count > 1 && identificationRecs.Count > 0)
            {
                var frm = new InputDialogBox(recs, images[0].GetImage().ToByteArray(), identificationRecs, toolStripStatusLabel2.Text.Substring(toolStripStatusLabel2.Text.LastIndexOf(":") + 1).Trim());
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = null;
                    saveToolStripMenuItem.Enabled = false;
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Please make sure you have 1 purple box and 2 Red boxes");
            }
        }

        private void ProcessDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var count = 0;
            foreach (OcrImage ocrImage in images)
            {
                try
                {
                    count++;
                    pictureBox1.Image = ocrImage.GetImage();
                    pictureBox1.Refresh();
                    UpdateStatusBar("Processing Image... " + count + " of " + images.Count());
                    var template = FindTemplate(ocrImage);
                    DrawRectangles(template.Data.ToRecs(), Color.Red, "Data");
                    DrawIdentifications(template.IdentificationData.ToRecs(), Color.Purple, "Identification");
                    Application.DoEvents();
                    var data = ocrImage.IdentifyData(template.Data.ToRecs());
                    string dataField1, dataField2;
                    GetIdentificationFields(data, out dataField1, out dataField2);
                    var staffMemeber = employees.GetEmployee(dataField1, dataField2);
                    if (staffMemeber == null)
                    {
                        MessageBox.Show($"No data found for DataField1: {dataField1}, DataField2: {dataField2}");
                        ocrImage.SaveFileAsError($"{dataField1} {dataField2 }_{ Guid.NewGuid().ToString()}_ERROR");
                    }
                    else
                    {
                        if(staffMemeber.PinCode.Trim().Length>0)
                            ocrImage.EncryptFile(staffMemeber.PinCode);
                         
                        var fileName = ocrImage.SaveFile(staffMemeber.DataField1 + "_" + Guid.NewGuid());
                        if (ConfigurationManager.AppSettings["UseEmail"].ToLower() == "true")
                        {
                            EmailSlip(staffMemeber.EmailAddress,fileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ocrImage.SaveFileAsError(Guid.NewGuid().ToString() + "_ERROR");
                    MessageBox.Show(ex.Message);
                }
            }
            pictureBox1.Image = null;
        }

        private void ClearTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Templates.RemoveAll();
            MessageBox.Show("Templates Removed");
        }

        private void EmployeeDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmDatabase();
            frm.ShowDialog(this);
        }

        private async Task EmailSlip(string emailAddress, string attachmentLocation)
        {
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("Payslips@absolutesys.com", "Payslip");
            var to = new EmailAddress(emailAddress);
            var msg = MailHelper.CreateSingleEmail(from, to, ConfigurationManager.AppSettings["EmailSubject"],"", ConfigurationManager.AppSettings["EmailMessage"]);
            var bytes = File.ReadAllBytes(attachmentLocation);
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("payslip.pdf", file);
            await client.SendEmailAsync(msg);
        }

        private void configurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmConfigurations();
            frm.ShowDialog(this);
        }
    }


}
