using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Browser;

namespace Sl.PassiveView
{

    public class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainView mainView = new MainView();
            Uri uri = new Uri("MainView.xaml", UriKind.Relative);
            Application.LoadComponent(mainView, uri);
            RootVisual = mainView;
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            HtmlPage.Window.Alert(e.ExceptionObject.ToString());
        }

    }
}