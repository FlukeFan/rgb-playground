using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sl.PassiveView
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

            ShowMessageControls(Visibility.Collapsed);
            PopulateColourSelection();

            _view.ShowMessage.Click += new RoutedEventHandler(ShowMessage_Click);
            _view.HideMessage.Click += new RoutedEventHandler(HideMessage_Click);
            _view.ColourSelection.SelectionChanged += new SelectionChangedEventHandler(ColourSelection_SelectionChanged);
        }

        private void ColourSelection_SelectionChanged(object sender, EventArgs e)
        {
            _view.Message.Foreground = new SolidColorBrush(FindMessageColour((SelectedColour)_view.ColourSelection.SelectedIndex));
        }

        private void HideMessage_Click(object sender, EventArgs e)
        {
            MessageBoxResult result =
                _view.DialogHandler.ShowMessageBox( "Are you sure?",
                                                    "Check", 
                                                    MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                ShowMessageControls(Visibility.Collapsed);
                _view.ShowMessage.IsEnabled = true;
            }
        }

        private void ShowMessage_Click(object sender, EventArgs e)
        {
            ShowMessageControls(Visibility.Visible);
            _view.ShowMessage.IsEnabled = false;
        }

        private void ShowMessageControls(Visibility visibility)
        {
            _view.Message.Visibility = visibility;
            _view.Message.Visibility = visibility;
            _view.SelectColourMessage.Visibility = visibility;
            _view.ColourSelection.Visibility = visibility;
            _view.HideMessage.Visibility = visibility;
        }

        private void PopulateColourSelection()
        {
            _view.ColourSelection.Items.Clear();

            _view.ColourSelection.Items.Add(((SelectedColour)0).ToString());
            _view.ColourSelection.Items.Add(((SelectedColour)1).ToString());
            _view.ColourSelection.Items.Add(((SelectedColour)2).ToString());

            _view.ColourSelection.SelectedIndex = 0;
        }

        private Color FindMessageColour(SelectedColour selectedColour)
        {
            if (selectedColour == SelectedColour.Black) return Colors.Black;
            if (selectedColour == SelectedColour.Green) return Colors.Green;
            if (selectedColour == SelectedColour.Red)   return Colors.Red;

            throw new Exception("unrecognised SelectedColour " + selectedColour.ToString());
        }

    }

}