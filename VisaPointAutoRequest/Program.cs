using System;
using System.Windows.Forms;
using System.IO;
using log4net;
using log4net.Config;

namespace VisaPointAutoRequest
{
    static class Program
    {
        // Logger
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledExceptionHandler;

            // Config log4net
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.xml");
            XmlConfigurator.Configure(new FileInfo(path));
            NDC.Push(string.Empty);

            // Create app folders
            CreateAppFolders();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void CurrentDomainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error(e.ExceptionObject);
        }

        private static void CreateAppFolders()
        {
            IOUtil.CreateDirectory("tmp", true);
        }
    }
}
