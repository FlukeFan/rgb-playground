
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Resources;

namespace DemoButton.Pages
{
    public class MyButton : UserControl
    {

        private DependencyObject _child;
        private Button _button;
        private TextBlock _textBlock;

        public event RoutedEventHandler Click;

        public MyButton()
        {
            _child = (DependencyObject)XamlReader.Load(typeof(MyButton).Assembly.GetManifestResourceStream("DemoButton.Pages.MyButton.xaml"));
            AddChild(_child);

            _button = (Button) NameScope.GetNameScope(_child).FindName("Button");
            _button.Click += Button_Click;

            _textBlock = (TextBlock) NameScope.GetNameScope(_child).FindName("TextBlock");
            _textBlock.Text = "text not set";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click(this, e);
        }

        public string MyText
        {
            set { _textBlock.Text = value; }
        }

    }
}