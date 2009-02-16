﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Browser;

namespace SlUserControl
{

    public class App : System.Windows.Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.LoadComponent(this, new Uri("App.xaml", UriKind.Relative));
            Uri uri = new Uri("Main.xaml", UriKind.Relative);

            // Load the main control
            Main main = new Main();
            System.Windows.Application.LoadComponent(main, uri);
            RootVisual = main;
        }

        private void Application_Exit(object sender, EventArgs e)
        {
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());
        }

    }
}