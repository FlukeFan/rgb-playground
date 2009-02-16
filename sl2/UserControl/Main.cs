
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SlUserControl
{

    public class Main : UserControl
    {

        private Button _test1;
        private ScrollViewer _scrollViewer;
        private StackPanel _messages;
        private DataGrid _grid;

        public Main()
        {
            Loaded += Main_Loaded;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            _scrollViewer = (ScrollViewer)FindName("_scrollViewer");
            _messages = (StackPanel)FindName("_messages");
            _test1 = (Button)FindName("_test1");
            _grid = (DataGrid)FindName("_grid");

            Write("Loaded");
        }

        private void Write(string message)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = DateTime.Now + " - " + message;
            _messages.Children.Add(textBlock);
            _scrollViewer.ScrollToVerticalOffset(_scrollViewer.ExtentHeight);
        }

    }
}