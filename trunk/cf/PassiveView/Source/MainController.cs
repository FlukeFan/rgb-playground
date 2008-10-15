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
            _view.HideMessage.Click += new EventHandler(HideMessage_Click);
            _view.ColourSelection.SelectedIndexChanged += new EventHandler(ColourSelection_SelectedIndexChanged);
        }

        private void ColourSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeMessageColour((SelectedColour)_view.ColourSelection.SelectedIndex);
        }

        private void HideMessage_Click(object sender, EventArgs e)
        {
            DialogResult result =
                _view.DialogHandler.ShowMessageBox( "Are you sure?",
                                                    "Check", 
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question,
                                                    MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                ShowMessageControls(false);
                _view.ShowMessage.Enabled = true;
            }
        }

        private void ShowMessage_Click(object sender, EventArgs e)
        {
            ShowMessageControls(true);
            PopulateColourSelection();
            _view.ShowMessage.Enabled = false;
        }

        private void ShowMessageControls(bool visible)
        {
            _view.SetVisible(_view.Message, visible);
            _view.SetVisible(_view.SelectColourMessage, visible);
            _view.SetVisible(_view.ColourSelection, visible);
            _view.SetVisible(_view.HideMessage, visible);
        }

        private void PopulateColourSelection()
        {
            _view.ColourSelection.Items.Clear();

            _view.ColourSelection.Items.Add(((SelectedColour)0).ToString());
            _view.ColourSelection.Items.Add(((SelectedColour)1).ToString());
            _view.ColourSelection.Items.Add(((SelectedColour)2).ToString());

            _view.ColourSelection.SelectedIndex = 0;
        }

        private void ChangeMessageColour(SelectedColour selectedColour)
        {
            if (selectedColour == SelectedColour.Black) _view.Message.ForeColor = Color.Black;
            if (selectedColour == SelectedColour.Green) _view.Message.ForeColor = Color.Green;
            if (selectedColour == SelectedColour.Red)   _view.Message.ForeColor = Color.Red;
        }

    }

}