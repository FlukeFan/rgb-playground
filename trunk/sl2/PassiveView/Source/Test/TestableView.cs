using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

using NUnit.Framework;

namespace Sl.PassiveView.Test
{

    public class TestableView : MainView
    {

        public TestableView() : this(null) { }

        public TestableView(DialogHandler dialogHandler)
        {
            ShowMessage = new Button();
            Message = new TextBlock();
            SelectColourMessage = new TextBlock();
            ColourSelection = new ComboBox();
            HideMessage = new Button();
            _dialogHandler = dialogHandler;
            new MainController(this);
        }

    }

}
