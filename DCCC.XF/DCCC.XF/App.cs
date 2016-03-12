using System;
using System.Linq;
using Xamarin.Forms;

namespace DCCC.XF
{
    public class App : Application
    {
        public App()
        {
            var label = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Bigsby in XForms"
            };

            var entry = new Entry();
            entry.Opacity = 0;
            entry.Unfocused += (s, e) => entry.Focus();
            entry.TextChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(e.NewTextValue) || e.NewTextValue == e.OldTextValue)
                    return;

                var previousTextLength = string.IsNullOrEmpty(e.OldTextValue) ? 0 : e.OldTextValue.Length;

                if (e.NewTextValue.Length != previousTextLength + 1)
                    return;

                var newText = e.NewTextValue.ToUpper().ToCharArray().Last();

                switch (newText)
                {
                    case 'A':
                        label.Text = "Left!";
                        break;
                    case 'D':
                        label.Text = "Right!";
                        break;
                    case 'W':
                        label.Text = "Up!";
                        break;
                    case 'S':
                        label.Text = "Down!";
                        break;
                }

                if (e.NewTextValue.Length > 100)
                    entry.Text = string.Empty;
            };

            var stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Fill,
                Children = {
                        label,
                        entry
                    },
                BackgroundColor = Color.Gray,


            };
            var contentPage = new ContentPage
            {

                Content = stackLayout,
                BackgroundColor = Color.Yellow
            };

            // The root page of your application
            MainPage = contentPage;

            AddGestureRecognizers(stackLayout, label);


        }

        private void AddGestureRecognizers(View container, Label displayer)
        {
            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += (s, e) => displayer.Text = "I was tapped!";
            container.GestureRecognizers.Add(tapRecognizer);

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
                            displayer.Text = totalX > 0 ? "Swiped Right!" : "Swiped Left!";
                        else
                            displayer.Text = totalY > 0 ? "Swiped Down!" : "Swiped Up!";

                        break;
                }
            };
            container.GestureRecognizers.Add(panRecognizer);

            var pinchRecognizer = new PinchGestureRecognizer();
            pinchRecognizer.PinchUpdated += (s, e) =>
            {
                switch (e.Status)
                {
                    case GestureStatus.Started:
                        break;
                    case GestureStatus.Running:
                        displayer.Text = string.Format("Pinched {0} in {1}.", e.Scale, e.ScaleOrigin);
                        break;
                    case GestureStatus.Completed:
                        //displayer.Text = string.Format("Pinched {0} in {1}.", e.Scale, e.ScaleOrigin);
                        break;
                    case GestureStatus.Canceled:
                        break;
                    default:
                        break;
                }
            };
            container.GestureRecognizers.Add(pinchRecognizer);
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
