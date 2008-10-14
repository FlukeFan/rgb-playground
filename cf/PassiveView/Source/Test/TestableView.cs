using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using NUnit.Framework;

namespace Cf.PassiveView.Source.Test
{

    public class TestableView : MainView
    {

        private Dictionary<Control, bool> _controlVisibility = new Dictionary<Control, bool>();

        public override void SetVisible(Control control, bool isVisible)
        {
            _controlVisibility[control] = isVisible;
        }

        public override bool IsVisible(Control control)
        {
            if (!_controlVisibility.ContainsKey(control))
                Assert.Fail("Control '" + control.Name + "' visibility has not been set by SetVisible()");

            return _controlVisibility[control];
        }

    }

}
