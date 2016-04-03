using DCCC.Interfaces;
using DCCC.XF.GameControls;
using Xamarin.Forms;

namespace DCCC.XF
{
    public class App : Application
    {
        private readonly GamePage _gamePage;
        private readonly GameApplication _app;

        public App()
        {
            _app = new GameApplication(
                new XFInputManager(_gamePage = new GamePage()),
                new XFLocalStorageManager());
            MainPage = _gamePage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            //_app.SaveState();
        }

        protected override void OnResume()
        {
            _app.Resume();
            // Handle when your app resumes
        }

        public static Size ScreenSize { get; private set; }
        public static void SetSize(Size size)
        {
            ScreenSize = size;
        }
    }
}
