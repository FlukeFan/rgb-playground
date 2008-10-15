using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cf.PassiveView.Source
{
    public partial class MainView : Form
    {

        protected DialogHandler _dialogHandler;

        public MainView()
        {
            InitializeComponent();
            _dialogHandler = new DialogHandler();
            new MainController(this);
        }

        public DialogHandler DialogHandler { get { return _dialogHandler; } }

        public virtual void SetVisible(Control control, bool isVisible)
        {
            control.Visible = isVisible;
        }

        public virtual bool IsVisible(Control control)
        {
            return control.Visible;
        }

    }
}