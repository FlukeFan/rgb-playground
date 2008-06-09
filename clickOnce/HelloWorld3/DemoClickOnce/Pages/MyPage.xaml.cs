﻿
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Resources;

namespace DemoClickOnce.Pages
{
    public class MyPage : UserControl
    {

        private DependencyObject _child;
        private Button _sendButton;
        private TextBlock _responseText;

        public MyPage()
        {
            _child = (DependencyObject)XamlReader.Load(typeof(MyPage).Assembly.GetManifestResourceStream("DemoClickOnce.Pages.MyPage.xaml"));
            AddChild(_child);

            _sendButton = (Button) NameScope.GetNameScope(_child).FindName("SendButton");
            _responseText = (TextBlock) NameScope.GetNameScope(_child).FindName("ResponseText");

            SendButton.Click += SendButton_Click;
        }

        public Button SendButton        { get { return _sendButton; } }
        public TextBlock ResponseText   { get { return _responseText; } }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ResponseText.Text = "Hello world 5 at " + DateTime.Now.ToString() + ":" + DateTime.Now.TimeOfDay.ToString() + "\n";
            ResponseText.Text += "Hello world 5 at " + DateTime.Now.ToString() + ":" + DateTime.Now.TimeOfDay.ToString() + "\n";
            ResponseText.Text += "Hello world 5 at " + DateTime.Now.ToString() + ":" + DateTime.Now.TimeOfDay.ToString() + "\n";
        }

    }
}