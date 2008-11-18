using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

using NUnit.Framework;

namespace Sl.PassiveView.Test
{

    public class TestableView : MainView
    {

        public TestableView()
        {
            ShowMessage = new Button();
            Message = new TextBlock();
            SelectColourMessage = new TextBlock();
            ColourSelection = new ComboBox();
            HideMessage = new Button();
            new MainController(this);
        }

    }

}
