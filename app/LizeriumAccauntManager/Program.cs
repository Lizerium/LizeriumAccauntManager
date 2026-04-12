/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 апреля 2026 14:16:02
 * Version: 1.0.6
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;


namespace Root
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");
            // Parse command line arguments
            string[] args = Environment.GetCommandLineArgs();
            foreach (string arg in args)
            {
                // Notify the application to run a player file clean as soon as possible.
                if (arg == "-autoclean")               
                {
                    RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\" + Application.ProductName);
                    key.SetValue("AutocleanPending", 1);
                    return;
                }
            }

            // Start the application if it is not already running.
            bool firstInstance;
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, "Local\\DSAccountManager-Running", out firstInstance);
            if (firstInstance)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
        }
    }
}
