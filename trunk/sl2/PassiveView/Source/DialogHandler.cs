using System;
using System.Windows;

namespace Sl.PassiveView
{

    public class DialogHandler
    {

        public virtual MessageBoxResult ShowMessageBox( string              messageBoxText,
                                                        string              caption,
                                                        MessageBoxButton    button)
        {
            return MessageBox.Show(messageBoxText, caption, button);
        }

    }

}