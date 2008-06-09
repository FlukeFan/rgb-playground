
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Net;
using System.Reflection;
using System.Windows;

namespace ClickOnceTest
{

    public class App : Application
    {

        // Entry point method
        [STAThread]
        public static void Main()
        {
            try
            {
                //Uri uri = new Uri("Run3.html", UriKind.Relative);
                //WebClient wc = new WebClient();
                //string output = wc.DownloadString(uri);
                //MessageBox.Show("Hello world 1: " + output);
                Type type = typeof(App);
                Assembly a = type.Assembly;
                MessageBox.Show(a.Location);
                //MessageBox.Show("Hello world 1");
            }
            catch(Exception e)
            {
                MessageBox.Show("error: " + e.ToString());
            }
        }

    }
}