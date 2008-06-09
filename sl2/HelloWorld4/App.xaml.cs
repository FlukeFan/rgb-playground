using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace System.Collections
{
    public class ArrayList
    {
    }
}

namespace Sl2
{

    public class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            //Uri uri = new Uri("App.xaml");
            //System.Windows.Application.LoadComponent(this, uri);

            //this.StartupUri = new System.Uri("Window1.xaml", System.UriKind.Relative);
            //InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //string xaml =
            //Page page = (Page)XamlReader.Load("Page.xaml");
            Uri uri = new Uri("Page.xaml", UriKind.Relative);
            //StreamResourceInfo stream = GetResourceStream(uri);
            //StreamReader reader = new StreamReader(stream.Stream);
            //string xaml = reader.ReadToEnd();
            //IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            //HtmlPage.Window.Alert(file.FileExists("/Page.xaml").ToString());
            //HtmlPage.Window.Alert(file.FileExists("/Pag2e.xaml").ToString());

            // Load the main control
            Page page = new Page();
            Button button = new Button();
            System.Windows.Application.LoadComponent(page, uri);
            //(Page)XamlReader.Load(xaml, true);
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