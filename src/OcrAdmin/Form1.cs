using BusinessLayer;
using DatabaseLayer;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        OcrTemplates Templates = new OcrTemplates();
        Employees employees = new Employees();

        Rectangle? rec = new Rectangle();
        List<OcrImage> images = new List<OcrImage>();
        List<Rectangle> recs = new List<Rectangle>();
        List<Rectangle> identificationRecs = new List<Rectangle>();
        IEnumerable<OcrTemplate> tempslateList;

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
            LoadTemplates();
            panel1.AutoScroll = true;
            LoadImages();
            recs.Clear();
            identificationRecs.Clear();

            btnCreateTemplate.Enabled = (identificationRecs.Count > 0 && recs.Count > 1);
        }

        private void LoadTemplates()
        {
            tempslateList = Templates.GetTemplates();
            cboTemplates.Items.Clear();
            cboTemplates.Items.AddRange(tempslateList.Select(x => new { x.Id, x.TemplateDescription }).ToArray());
        }

        private void LoadImages()
        {
            UpdateStatusBar("Loading Images");
            images.Clear();
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            foreach (string f in Directory.GetFiles(ConfigurationManager.AppSettings.Get("ApplicationDirectory"), "*.pdf"))
            {
                UpdateStatusBar("Loading Image : " + f);
                images.Add(new OcrImage(f, new MySmtp()));
            }

            if (images.Any())
                pictureBox1.Image = images[0].GetImage();

            toolStripStatusLabel1.Text = "Total Pages : " + images.Count;
            btnIdentify.Enabled = (this.images.Any());
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "X=" + e.Location.X.ToString() + " Y=" + e.Location.Y.ToString();
            if (identificationRecs.Count == 1 && e.Button == MouseButtons.Right || recs.Count == 3)
            {
                MessageBox.Show("You are only allowed 1 Purple and 3 Red boxes");
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
                string name, code;
                ShowData(data, out name, out code);
            }
            else
            {
                recs.Add(rec.Value);
            }
            rec = null;
            DrawRectangles(recs, Color.Red, "Data");
            DrawIdentifications(identificationRecs, Color.Purple, "Identification");
            btnCreateTemplate.Enabled = (identificationRecs.Count > 0 && recs.Count > 1);
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

        private void BtnIdentify_Click(object sender, EventArgs e)
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
                    ShowData(data, out dataField1, out dataField2);
                    var staffMemeber = employees.GetEmployee(dataField1, dataField2);
                    if (data == null)
                    {
                        MessageBox.Show(String.Format("No data found for DataField1: {0}, DataField2: {1}", dataField1, dataField2));
                    }
                    else
                    {
                        ocrImage.EncryptFile(staffMemeber.PinCode);
                        ocrImage.SaveFile(staffMemeber.DataField1 + "_" + Guid.NewGuid());
                        ocrImage.Send(staffMemeber.EmailAddress);
                        //MessageBox.Show(String.Format("This document will be emailed to : {0}", res.EmailAddress));
                    }
                }
                catch (Exception ex)
                {
                    ocrImage.SaveFileAsError(Guid.NewGuid().ToString() + "_ERROR");
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ShowData(IEnumerable<string> data, out string name, out string code)
        {
            try
            {
                name = data.ToList()[0];
                code = data.ToList()[1];
            }
            catch (Exception ex)
            {
                name = "";
                code = "";
            }
            finally
            {
                listBox1.Items.Clear();
                foreach (var s in data)
                {
                    listBox1.Items.Add(s);
                }
            }
        }

        private OcrTemplate FindTemplate(OcrImage ocrImage)
        {
            List<string> info = new List<string>();
            foreach (var t in tempslateList)
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

        private void BtnCreateTemplate_Click(object sender, EventArgs e)
        {
            if (recs.Count > 1 && identificationRecs.Count > 0)
            {
                var frm = new InputDialogBox(recs, images[0].GetImage().ToByteArray(), identificationRecs);
                frm.ShowDialog();
                LoadTemplates();
            }
            else
            {
                MessageBox.Show("Please make sure you have 1 purple box and 2 Red boxes");
            }
        }

        private void CboTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            recs.Clear();
            identificationRecs.Clear();
            var template = Templates.GetTemplate(cboTemplates.Text.ToString());
            var data = JsonConvert.DeserializeObject<Rectangle[]>(template.Data);
            recs.AddRange(data);
            identificationRecs.AddRange(JsonConvert.DeserializeObject<Rectangle[]>(template.IdentificationData));
            DrawRectangles(recs, Color.Red, "Data");
            DrawIdentifications(identificationRecs, Color.Purple, "Identification");
        }

        private void EmployeeRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "CSV|*.csv";
            var res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                var d = dlg.FileName;
                var file = File.ReadAllLines(d);
                foreach (string s in file.Skip(1))
                {
                    employees.AddNewEmployee(s.Split(',')[0], s.Split(',')[1], s.Split(',')[2], s.Split(',')[3]);
                }
                MessageBox.Show("Data Updated");
            }
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
    }


}
