using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cf.PassiveView.Source
{

    public enum SelectedColour
    {
        Black,
        Green,
        Red,
    }

    internal class MainController
    {

        private MainView _view;

        public MainController(MainView view)
        {
            _view = view;

            ShowMessageControls(false);

            _view.ShowMessage.Click += new EventHandler(ShowMessage_Click);
        }

        private void ShowMessageControls(bool visible)
        {
            _view.SetVisible(_view.Message, visible);
            _view.SetVisible(_view.SelectColourMessage, visible);
            _view.SetVisible(_view.ColourSelection, visible);
            _view.SetVisible(_view.HideMessage, visible);
        }

        private void ShowMessage_Click(object sender, EventArgs e)
        {
            ShowMessageControls(true);
            PopulateColourSelection();
            _view.ShowMessage.Enabled = false;
        }

        private void PopulateColourSelection()
        {
            _view.ColourSelection.Items.Clear();

            _view.ColourSelection.Items.Add(((SelectedColour)0).ToString());
            _view.ColourSelection.Items.Add(((SelectedColour)1).ToString());
            _view.ColourSelection.Items.Add(((SelectedColour)2).ToString());

            _view.ColourSelection.SelectedIndex = 0;
        }

    }

}