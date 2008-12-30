using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Browser;

namespace SlWcf
{

    public class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Uri uri = new Uri("Page.xaml", UriKind.Relative);

            // Load the main control
            Page page = new Page();
            System.Windows.Application.LoadComponent(page, uri);
            RootVisual = page;
        }

        private void Application_Exit(object sender, EventArgs e)
        {
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            HtmlPage.Window.Alert(e.ExceptionObject.ToString());
        }

    }
}