using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class InputDialogBox : Form
    {
        private List<Rectangle> recs;
        private readonly List<Rectangle> IdentificationRecs;

        public byte[] OcrImage { get; }

        public InputDialogBox(List<Rectangle> recs, byte[] ocrImage, List<Rectangle> identificationRecs)
        {
            InitializeComponent();
            this.recs = recs;
            OcrImage = ocrImage;
            IdentificationRecs = identificationRecs;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (txtTemplateName.Text.Trim().Length > 0)
            {
                var Templates = new OcrTemplates();
                Templates.AddNewTemplate(txtTemplateName.Text, Newtonsoft.Json.JsonConvert.SerializeObject(recs), OcrImage, Newtonsoft.Json.JsonConvert.SerializeObject(IdentificationRecs));
                this.Dispose();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
