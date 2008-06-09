
using System;
using System.Collections.Generic;
using System.Windows;

namespace Xbap
{

    public class App : Application
    {

        // Entry point method
        [STAThread]
        public static void Main()
        {
            MessageBox.Show("Hello world 1");
            App app = new App();
            //app.Run();
        }

    }
}