using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Sl.PassiveView
{
    public class MainView : UserControl
    {

        protected DialogHandler _dialogHandler;

        public Button ShowMessage;
        public TextBlock Message;
        public TextBlock SelectColourMessage;
        public ComboBox ColourSelection;
        public Button HideMessage;

        public MainView()
        {
            Loaded += new RoutedEventHandler(UserControl_Loaded);
        }

        public DialogHandler DialogHandler { get { return _dialogHandler; } }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ShowMessage = (Button)FindName("ShowMessage");
            Message = (TextBlock)FindName("Message");
            SelectColourMessage = (TextBlock)FindName("SelectColourMessage");
            ColourSelection = (ComboBox)FindName("ColourSelection");
            HideMessage = (Button)FindName("HideMessage");
            _dialogHandler = new DialogHandler();
            new MainController(this);
        }

    }
}