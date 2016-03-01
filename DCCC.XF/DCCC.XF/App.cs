using System;
using Xamarin.Forms;

namespace DCCC.XF
{
    public class App : Application
    {
        private readonly Label _label;

        public App()
        {
            _label = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Bigsby in XForms"
            };

            var panRecognizer = new PanGestureRecognizer();
            double totalX = 0;
            double totalY = 0;

            panRecognizer.PanUpdated += (s, e) =>
            {
                switch (e.StatusType)
                {
                    case GestureStatus.Canceled:
                    case GestureStatus.Started:
                        totalX = totalY = 0;
                        break;
                    case GestureStatus.Running:
                        totalX += e.TotalX;
                        totalY += e.TotalY;
                        break;
                    case GestureStatus.Completed:
                        if (totalX == 0 && totalY == 0) return;

                        if (Math.Abs(totalX) > Math.Abs(totalY))
                            _label.Text = totalX > 0 ? "Swiped Right!" : "Swiped Left!";
                        else
                            _label.Text = totalY > 0 ? "Swiped Down!" : "Swiped Up!";

                        break;
                }
            };

            var stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Fill,
                Children = {
                        _label
                    },
                BackgroundColor = Color.Pink,

            };

            stackLayout.GestureRecognizers.Add(panRecognizer);
            // The root page of your application
            MainPage = new ContentPage
            {

                Content = stackLayout,
                BackgroundColor = Color.Yellow
            };
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
