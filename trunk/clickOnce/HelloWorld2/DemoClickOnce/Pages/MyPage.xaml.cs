using System;
using System.Windows;
using System.Windows.Controls;

namespace DemoClickOnce.Pages
{
    public class MyPage : UserControl
    {

        public MyPage()
        {
            Initialized += MyPage_Initialized;
        }

        private void MyPage_Initialized(object sender, EventArgs e)
        {
            SendButton.Click += SendButton_Click;
        }

        public Button SendButton        { get { return (Button) FindName("SendButton"); } }
        public TextBlock ResponseText   { get { return (TextBlock) FindName("ResponseText"); } }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ResponseText.Text = "Hello world 5 at " + DateTime.Now.ToString() + ":" + DateTime.Now.TimeOfDay.ToString() + "\n";
            ResponseText.Text += "Hello world 5 at " + DateTime.Now.ToString() + ":" + DateTime.Now.TimeOfDay.ToString() + "\n";
            ResponseText.Text += "Hello world 5 at " + DateTime.Now.ToString() + ":" + DateTime.Now.TimeOfDay.ToString() + "\n";
        }

    }
}