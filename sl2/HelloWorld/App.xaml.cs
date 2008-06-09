using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Sl2
{

    public class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;


HtmlPage.Window.Alert("Hello world");
            //Uri uri = new Uri("App.xaml");
            //System.Windows.Application.LoadComponent(this, uri);

            //this.StartupUri = new System.Uri("Window1.xaml", System.UriKind.Relative);
            //InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Load the main control
            //this.RootVisual = new Page();
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {

        }

    }
}