using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class frmConfigurations : Form
    {
        NameValueCollection settings = ConfigurationManager.AppSettings;
        public frmConfigurations()
        {
            InitializeComponent();
        }

        private void frmConfigurations_Load(object sender, EventArgs e)
        {
            txtApplicationDirectory.Text = settings["ApplicationDirectory"];
            txtEncryptionDirectory.Text = settings["EncryptionDirectory"];
            txtErrorDirectory.Text = settings["ErrorDirectory"];
            chkDelete.Checked = settings["DeleteUnencryptedFiles"] == "true";
            txtHost.Text = settings["Host"];
            txtPort.Text = settings["SmtpPort"];
            txtApiKey.Text = settings["ApiKey"];
            chkEmail.Checked = settings["UseEmail"] == "true";
            txtEmailSubject.Text = settings["EmailSubject"];
            txtEmailMessage.Text = settings["EmailMessage"];
            txtFromEmailAddress.Text = settings["FromEmailAddress"];
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ApplicationDirectory"].Value = txtApplicationDirectory.Text;
            config.AppSettings.Settings["EncryptionDirectory"].Value = txtEncryptionDirectory.Text;
            config.AppSettings.Settings["ErrorDirectory"].Value = txtErrorDirectory.Text;
            config.AppSettings.Settings["Host"].Value = txtHost.Text;
            config.AppSettings.Settings["SmtpPort"].Value = txtPort.Text;
            config.AppSettings.Settings["ApiKey"].Value = txtApiKey.Text;
            config.AppSettings.Settings["UseEmail"].Value = (chkEmail.Checked ? "true" : "false");
            config.AppSettings.Settings["DeleteUnencryptedFiles"].Value = (chkDelete.Checked ? "true" : "false");
            config.AppSettings.Settings["EmailSubject"].Value = txtEmailSubject.Text;
            config.AppSettings.Settings["EmailMessage"].Value = txtEmailMessage.Text;
            config.AppSettings.Settings["FromEmailAddress"].Value = txtFromEmailAddress.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            this.Close();
        }
    }
}
