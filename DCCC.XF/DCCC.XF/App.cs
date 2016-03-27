using System;
using System.Linq;
using Xamarin.Forms;

namespace DCCC.XF
{
    public class App : Xamarin.Forms.Application
    {
        GamePage _gamePage;
        public App()
        {
            var app = new Application(new XFInputManager(_gamePage = new GamePage()));
            MainPage = _gamePage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
