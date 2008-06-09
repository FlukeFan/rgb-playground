
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xml;
using Demo.Pages;

namespace Demo
{

    public class App : Application
    {

        // Entry point method
        [STAThread]
        public static void Main()
        {
            try
            {
                App app = new App();
                app.Run();
            }
            catch(Exception e)
            {
                MessageBox.Show("error: " + e.ToString());
            }
        }

        public App()
        {
            Startup += Application_Startup;
            DispatcherUnhandledException += Application_UnhandledException;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new Window();
            MainWindow.Title = "Atlanta Library Project";
            MainWindow.Show();

            MyPage myPage = new MyPage();
            MainWindow.Content = myPage;
        }

        private void Application_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
        }
    }
}