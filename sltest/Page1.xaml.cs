//------------------------------------------------------------------------------
// <copyright file="default.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Configuration;

using Atlanta.Application.Domain.Lender;

namespace SampleApplication
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Controls;
    using System.Windows.Shapes;

    public partial class EventHandlingCanvas : Canvas
    {
        public void Page_Loaded(object o, EventArgs e)
        {
            // Required to initialize variables
            //InitializeComponent();
        }
        public EventHandlingCanvas()
        {
            this.Loaded+=new EventHandler(EventHandlingCanvas_Loaded);
        }
        void EventHandlingCanvas_Loaded(object sender, EventArgs e)
        {
            //Button2.MouseLeftButtonUp+=new MouseEventHandler(OnClick);
        }
        void OnClick(object sender, MouseEventArgs e)
        {
            Canvas cc = sender as Canvas;
            SolidColorBrush sb = new SolidColorBrush();
            sb.Color = Colors.Red;
            cc.Background = sb;
            TextBlock tb = cc.Children[0] as TextBlock;
            Media media = Media.InstantiateOrphanedMedia(MediaType.Dvd, "name", "desc");
            tb.Text = media.Type.ToString() + ConfigurationManager.AppSettings["slvar"];
        }
    }
}
