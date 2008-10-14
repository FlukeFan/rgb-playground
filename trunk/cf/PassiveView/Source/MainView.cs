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

        public MainView()
        {
            InitializeComponent();
            new MainController(this);
        }

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