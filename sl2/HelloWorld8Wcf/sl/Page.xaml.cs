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

        private ServiceClient client;

        public Page()
        {
            client = new ServiceClient();
            client.GetC1Completed += new EventHandler<GetC1CompletedEventArgs>(client_GetC1Completed);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            client.GetC1Async();
            TextBlock tb = (TextBlock)FindName("textResponse");
            tb.Text = "called";
        }

        private void client_GetC1Completed(object sender, GetC1CompletedEventArgs e)
        {
            TextBlock tb = (TextBlock)FindName("textResponse");
            if (e.Error != null)
            {
                tb.Text = e.Error.Message;
                throw e.Error;
            }
            else
            {
                tb.Text = e.Result.Value;
            }
        }

    }
}