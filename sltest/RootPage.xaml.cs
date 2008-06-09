
namespace FirstApplication
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Controls;
    using System.Windows.Shapes;

    public class RootPage : Canvas
    {

        public void Page_Loaded(object o, EventArgs e)
        {
            // Required to initialize variables
            //InitializeComponent();
        }

        public RootPage()
        {
            //this.Loaded += new RoutedEventHandler(EventHandlingCanvas_Loaded);
        }

        void EventHandlingCanvas_Loaded(object sender, EventArgs e)
        {
            //Button2.MouseLeftButtonUp+=new MouseEventHandler(OnClick);
        }

        void OnClick(object sender, MouseEventArgs e)
        {
            Canvas cc = sender as Canvas;
            SolidColorBrush sb = new SolidColorBrush();
            sb.Color = Colors.Red;
            cc.Background = sb;
            TextBlock tb = cc.Children[0] as TextBlock;
            tb.Text = "Clicked...";
        }
    }
}
