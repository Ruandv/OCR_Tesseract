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

        Rectangle rec = new Rectangle();
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
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "X=" + e.Location.X.ToString() + " Y=" + e.Location.Y.ToString();
            rec = new Rectangle(e.Location.X, e.Location.Y, 100, 100);
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                identificationRecs.Add(rec);

                var data = images[0].IdentifyData(new Rectangle[] { rec });
                string name, code;
                ShowData(data, out name, out code);
            }
            else
            {
                recs.Add(rec);
            }

            DrawRectangles(recs, Color.Red);
            DrawIdentifications(identificationRecs, Color.Purple);
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                rec = new Rectangle(rec.X, rec.Y, e.Location.X - rec.X, e.Location.Y - rec.Y);
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    pictureBox1.CreateGraphics().DrawRectangle(pen, rec);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                rec = new Rectangle(rec.X, rec.Y, e.Location.X - rec.X, e.Location.Y - rec.Y);
                using (Pen pen = new Pen(Color.Purple, 2))
                {
                    pictureBox1.CreateGraphics().DrawRectangle(pen, rec);
                }
            }
        }

        private void BtnIdentify_Click(object sender, EventArgs e)
        {
            try
            {
                var count = 0;
                foreach (OcrImage ocrImage in images)
                {
                    count++;
                    var template = FindTemplate(ocrImage);
                    pictureBox1.Image = ocrImage.GetImage();
                    pictureBox1.Refresh();
                    DrawRectangles(recs, Color.Red);
                    DrawIdentifications(identificationRecs, Color.Purple);
                    UpdateStatusBar("Processing Image... " + count + " of " + images.Count());
                    Application.DoEvents();
                    var data = ocrImage.IdentifyData(recs);
                    string name, code;
                    ShowData(data, out name, out code);
                    ocrImage.EncryptFile("");
                    ocrImage.SaveFile(DateTime.Now.ToOADate().ToString() + "_" + data.ToList()[1]);

                    ////var res = employees.GetEmployee(code, name);
                    //if (res == null)
                    //{
                    //    MessageBox.Show(String.Format("No data found for Name: {0}, Code: {1}", name, code));
                    //}
                    //else
                    //{
                    //    ocrImage.EncryptFile(res.IdentityNumber);
                    //    ocrImage.SaveFile("MyFile_" + Guid.NewGuid());
                    //    ocrImage.Send();
                    //    MessageBox.Show(String.Format("This document will be emailed to : {0}", res.EmailAddress));
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RenameFile()
        {
            throw new NotImplementedException();
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

        private string FindTemplate(OcrImage ocrImage)
        {
            foreach (var t in tempslateList)
            {
                var idFields = JsonConvert.DeserializeObject<Rectangle[]>(t.IdentificationData);
                ocrImage.IdentifyData(idFields);
            }
            return "S";
        }

        private void DrawIdentifications(IEnumerable<Rectangle> rectangles, Color penColor)
        {
            foreach (var r in rectangles)
            {
                using (Pen pen = new Pen(penColor, 2))
                {
                    pictureBox1.CreateGraphics().DrawRectangle(pen, r);
                }
            }
        }

        private void DrawRectangles(IEnumerable<Rectangle> rectangles, Color penColor)
        {
            pictureBox1.Refresh();
            DrawIdentifications(rectangles, penColor);
        }

        private void BtnUploadPdf_Click(object sender, EventArgs e)
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
                catch (System.ArgumentException ex)
                {
                    throw ex;
                }
            }
            sourceDocument.Close();
            reader.Close();
        }

        private void BtnCreateTemplate_Click(object sender, EventArgs e)
        {
            var frm = new InputDialogBox(recs, images[0].GetImage().ToByteArray(), identificationRecs);
            frm.ShowDialog();
            LoadTemplates();
        }

        private void CboTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            recs.Clear();
            identificationRecs.Clear();
            var template = Templates.GetTemplate(cboTemplates.Text.ToString());
            var data = JsonConvert.DeserializeObject<Rectangle[]>(template.Data);
            recs.AddRange(data);
            identificationRecs.AddRange(JsonConvert.DeserializeObject<Rectangle[]>(template.IdentificationData));
            DrawRectangles(recs, Color.Red);
            DrawIdentifications(identificationRecs, Color.Purple);
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

    }


}
