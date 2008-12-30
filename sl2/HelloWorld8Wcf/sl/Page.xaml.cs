﻿
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

namespace SlWcf
{
    public class Page : UserControl
    {

        private ServiceClient client;

        private Button _send;
        private List<string> _calls = new List<string>();
        private Storyboard _timer;

        public Page()
        {
            _timer = new Storyboard();
            client = new ServiceClient();
            client.GetC1Completed += new EventHandler<GetC1CompletedEventArgs>(client_GetC1Completed);
            Loaded += new RoutedEventHandler(Page_Loaded);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _send = (Button)FindName("Send");
            _send.Click += new RoutedEventHandler(Send_Click);

            _timer.Duration = new TimeSpan(0, 0, 5);
            _timer.Completed += new EventHandler(timer_Completed);
            _timer.Begin();
        }

        private void timer_Completed(object sender, EventArgs e)
        {
            Send_Click(null, null);
            _timer.Begin();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
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
                tb.Text = e.Error.ToString();
                throw e.Error;
            }
            else
            {
                tb.Text = e.Result.Value + ", " + e.Result.C2.Value;
            }
        }

    }
}