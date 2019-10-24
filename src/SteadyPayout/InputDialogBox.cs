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

        public InputDialogBox(List<Rectangle> recs, byte[] ocrImage, List<Rectangle> identificationRecs, string suggestedName)
        {
            InitializeComponent();
            this.recs = recs;
            OcrImage = ocrImage;
            txtTemplateName.Text = suggestedName;
            IdentificationRecs = identificationRecs;
        }

        private void CmdOk_Click(object sender, EventArgs e)
        {
            if (txtTemplateName.Text.Trim().Length > 0)
            {
                var Templates = new OcrTemplates();
                Templates.AddNewTemplate(txtTemplateName.Text, Newtonsoft.Json.JsonConvert.SerializeObject(recs), OcrImage, Newtonsoft.Json.JsonConvert.SerializeObject(IdentificationRecs));
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
