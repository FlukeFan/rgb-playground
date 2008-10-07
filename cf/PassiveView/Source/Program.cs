
using System;
using System.Windows.Forms;

namespace Cf.PassiveView.Source
{

    public static class Program
    {

        [MTAThread]
        public static void Main()
        {
            MainView view = new MainView();
            Application.Run(view);
        }

    }

}

