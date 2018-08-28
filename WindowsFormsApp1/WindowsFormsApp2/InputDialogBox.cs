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

        public InputDialogBox(List<Rectangle> recs)
        {
            InitializeComponent();
            this.recs = recs;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (txtTemplateName.Text.Trim().Length > 0)
            {
                var Templates = new OcrTemplates();
                Templates.AddNewTemplate(txtTemplateName.Text, Newtonsoft.Json.JsonConvert.SerializeObject(recs));
                this.Dispose();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
