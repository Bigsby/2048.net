using System;
using Windows.Foundation.Metadata;
using Xamarin.Forms;

namespace DCCC.XF.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            if (Device.Idiom == TargetIdiom.Desktop)
            {
                MinWidth = 600;
                MinHeight = 800;
            }
            LoadApplication(new XF.App());
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                statusBar.BackgroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);
                statusBar.ForegroundColor = Windows.UI.Color.FromArgb(255, 255, 255, 255);
                statusBar.ShowAsync().AsTask().Wait();
                //statusBar.BackgroundColor = Windows.UI.Colors.Transparent;
                //statusBar.ForegroundColor = Windows.UI.Colors.Red;
            }
        }
    }
}
