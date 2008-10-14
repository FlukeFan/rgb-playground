using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cf.PassiveView.Source
{

    public class DialogHandler
    {

        public virtual DialogResult ShowMessageBox( string                  text,
                                                    string                  caption,
                                                    MessageBoxButtons       buttons,
                                                    MessageBoxIcon          icon,
                                                    MessageBoxDefaultButton defaultButton)
        {
            return MessageBox.Show(text, caption, buttons, icon, defaultButton);
        }

    }

}