using System;
using System.Configuration;
using System.Windows.Forms;
using WindowsFormsApp2;
using BusinessLayer;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace SteadyPayout
{
    static class Program
    {
        private static Container container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            Application.Run(new Form1(container.GetInstance<ISmtpInfo>()));
        }

        private static void Bootstrap()
        {
            // Create the container as usual.
            container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            // Register your types, for instance:
            if (ConfigurationManager.AppSettings["EmailClientName"] == "Office365")
            {
                container.Register<ISmtpInfo, Office365>(Lifestyle.Singleton);
            }
            else
            {
                container.Register<ISmtpInfo, SendGrid>(Lifestyle.Singleton);
            }


            container.Register<Form1>(Lifestyle.Scoped);
            container.Register<FrmConfigurations>(Lifestyle.Scoped);
            container.Register<frmDatabase>(Lifestyle.Scoped);

            // Optionally verify the container.
            container.Verify();
        }
    }
}
