using System;
using Windows.Foundation.Metadata;
using Windows.UI;

namespace DCCC.XF.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new XF.App());
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                statusBar.BackgroundColor = Color.FromArgb(255, 0, 0, 0);
                statusBar.ForegroundColor = Color.FromArgb(255, 255, 255, 255);
                statusBar.ShowAsync().AsTask().Wait();
                //statusBar.BackgroundColor = Windows.UI.Colors.Transparent;
                //statusBar.ForegroundColor = Windows.UI.Colors.Red;
            }
        }
    }
}
