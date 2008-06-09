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
    public class Page : UserControl
    {

        public Page()
        {
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)FindName("textResponse");
            tb.Text = "Hello world 5 at " + DateTime.Now.ToString() + ":" + DateTime.Now.TimeOfDay.ToString() + "\n";
            tb.Text += "Hello world 5 at " + DateTime.Now.ToString() + ":" + DateTime.Now.TimeOfDay.ToString() + "\n";
            tb.Text += "Hello world 5 at " + DateTime.Now.ToString() + ":" + DateTime.Now.TimeOfDay.ToString() + "\n";
        }

    }
}