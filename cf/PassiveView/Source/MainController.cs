using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cf.PassiveView.Source
{

    internal class MainController
    {

        private MainView _view;

        public MainController(MainView view)
        {
            _view = view;

            _view.SetVisible(_view.Message, false);
            _view.SetVisible(_view.ColourSelection, false);
            _view.SetVisible(_view.HideMessage, false);
        }

    }

}