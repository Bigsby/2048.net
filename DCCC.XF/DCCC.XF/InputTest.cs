//using DCCC.Interfaces;
//using System;

//using Xamarin.Forms;

//namespace DCCC.XF
//{
//    public class InputTest : ContentPage
//    {
//        private Grid _grid;
//        private Label _display;
//        private Entry _kbInput;

//        public InputTest()
//        {
//            _display = new Label
//            {
//                Text = "Nothing...",
//                HorizontalOptions = LayoutOptions.Center,
//                VerticalOptions = LayoutOptions.Center
//            };


//            _grid = new Grid();
//            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
//            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
//            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
//            _grid.BackgroundColor = Color.Gray;
//            _grid.RowSpacing = 0;

//            var contentView = new ContentView
//            {
//                Content = _grid
//            };
//            Content = contentView;

//            var header = new Grid
//            {
//                BackgroundColor = Color.Blue
//            };
//            header.Children.Add(new Label { Text = "Header", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center });
//            _grid.Children.Add(header);

//            var content = new Grid
//            {
//                BackgroundColor = Color.Yellow
//            };
//            content.Children.Add(_display);

//            _kbInput = new Entry
//            {
//                Opacity = 0,
//                HeightRequest = 1,
//                WidthRequest = 1,
//                VerticalOptions = LayoutOptions.Start,
//                HorizontalOptions = LayoutOptions.Start
//            };

//            _kbInput.Unfocused += (s, e) => _kbInput.Focus();
//            _kbInput.TextChanged += (s, e) =>
//            {
//                var t = e.NewTextValue.ToUpper();
//                switch (t)
//                {
//                    case "A":
//                        Move(MoveDirection.Left);
//                        break;
//                    case "W":
//                        Move(MoveDirection.Up);
//                        break;
//                    case "D":
//                        Move(MoveDirection.Right);
//                        break;
//                    case "S":
//                        Move(MoveDirection.Down);
//                        break;
//                }

//                _kbInput.Text = string.Empty;
//            };
//            content.Children.Add(_kbInput);

//            _grid.Children.Add(content);
//            Grid.SetRow(content, 1);
//            var footer = new Grid
//            {
//                BackgroundColor = Color.Pink
//            };
//            footer.Children.Add(new Label { Text = "Footer", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center });
//            _grid.Children.Add(footer);
//            Grid.SetRow(footer, 2);

//            var panGesture = new PanGestureRecognizer();
//            var totalX = .0;
//            var totalY = .0;
//            panGesture.PanUpdated += (s, e) =>
//            {
//                switch (e.StatusType)
//                {
//                    case GestureStatus.Started:
//                        break;
//                    case GestureStatus.Running:
//                        totalX += e.TotalX;
//                        totalY += e.TotalY;
//                        break;
//                    case GestureStatus.Completed:
//                        if (Math.Abs(totalX) > Math.Abs(totalY))
//                            Move(totalX > 0 ? MoveDirection.Right : MoveDirection.Left);
//                        else
//                            Move(totalY > 0 ? MoveDirection.Down : MoveDirection.Up);

//                        totalX = totalY = 0;
//                        break;
//                    case GestureStatus.Canceled:
//                        break;
//                    default:
//                        break;
//                }
//            };
//            contentView.GestureRecognizers.Add(panGesture);

//            _kbInput.Focus();
//        }

//        private void Move(MoveDirection direction)
//        {
//            _display.Text = direction.ToString();
//        }
//    }

//    //private void TestPages()
//    //{
//    //    MainPage = new TestContentPage(() => GoTo(false));
//    //}

//    //private void GoTo(bool content)
//    //{
//    //    ContentPage newPage = null;

//    //    if (content)
//    //        newPage = new TestContentPage(() => GoTo(false));
//    //    else
//    //        newPage = new TestPage(() => GoTo(true));

//    //    MainPage = newPage;
//    //}

//    //private void SetGesturesPage()
//    //{
//    //    var label = new Label
//    //    {
//    //        HorizontalTextAlignment = TextAlignment.Center,
//    //        Text = "Bigsby in XForms"
//    //    };

//    //    var entry = new Entry();
//    //    entry.Opacity = 0;
//    //    entry.Unfocused += (s, e) => entry.Focus();
//    //    entry.TextChanged += (s, e) =>
//    //    {
//    //        if (string.IsNullOrEmpty(e.NewTextValue) || e.NewTextValue == e.OldTextValue)
//    //            return;

//    //        var previousTextLength = string.IsNullOrEmpty(e.OldTextValue) ? 0 : e.OldTextValue.Length;

//    //        if (e.NewTextValue.Length != previousTextLength + 1)
//    //            return;

//    //        var newText = e.NewTextValue.ToUpper().ToCharArray().Last();

//    //        switch (newText)
//    //        {
//    //            case 'A':
//    //                label.Text = "Left!";
//    //                break;
//    //            case 'D':
//    //                label.Text = "Right!";
//    //                break;
//    //            case 'W':
//    //                label.Text = "Up!";
//    //                break;
//    //            case 'S':
//    //                label.Text = "Down!";
//    //                break;
//    //        }

//    //        if (e.NewTextValue.Length > 100)
//    //            entry.Text = string.Empty;
//    //    };

//    //    var stackLayout = new StackLayout
//    //    {
//    //        VerticalOptions = LayoutOptions.Fill,
//    //        Children = {
//    //                label,
//    //                entry
//    //            },
//    //        BackgroundColor = Color.Gray,


//    //    };
//    //    var contentPage = new ContentPage
//    //    {

//    //        Content = stackLayout,
//    //        BackgroundColor = Color.Yellow
//    //    };

//    //    // The root page of your application
//    //    MainPage = contentPage;

//    //    AddGestureRecognizers(stackLayout, label);
//    //}

//    //private void AddGestureRecognizers(View container, Label displayer)
//    //{
//    //    var tapRecognizer = new TapGestureRecognizer();
//    //    tapRecognizer.Tapped += (s, e) => displayer.Text = "I was tapped!";
//    //    container.GestureRecognizers.Add(tapRecognizer);

//    //    var panRecognizer = new PanGestureRecognizer();
//    //    double totalX = 0;
//    //    double totalY = 0;
//    //    panRecognizer.PanUpdated += (s, e) =>
//    //    {
//    //        switch (e.StatusType)
//    //        {
//    //            case GestureStatus.Canceled:
//    //            case GestureStatus.Started:
//    //                totalX = totalY = 0;
//    //                break;
//    //            case GestureStatus.Running:
//    //                totalX += e.TotalX;
//    //                totalY += e.TotalY;
//    //                break;
//    //            case GestureStatus.Completed:
//    //                if (totalX == 0 && totalY == 0) return;

//    //                if (Math.Abs(totalX) > Math.Abs(totalY))
//    //                    displayer.Text = totalX > 0 ? "Swiped Right!" : "Swiped Left!";
//    //                else
//    //                    displayer.Text = totalY > 0 ? "Swiped Down!" : "Swiped Up!";

//    //                break;
//    //        }
//    //    };
//    //    container.GestureRecognizers.Add(panRecognizer);

//    //    var pinchRecognizer = new PinchGestureRecognizer();
//    //    pinchRecognizer.PinchUpdated += (s, e) =>
//    //    {
//    //        switch (e.Status)
//    //        {
//    //            case GestureStatus.Started:
//    //                break;
//    //            case GestureStatus.Running:
//    //                displayer.Text = string.Format("Pinched {0} in {1}.", e.Scale, e.ScaleOrigin);
//    //                break;
//    //            case GestureStatus.Completed:
//    //                //displayer.Text = string.Format("Pinched {0} in {1}.", e.Scale, e.ScaleOrigin);
//    //                break;
//    //            case GestureStatus.Canceled:
//    //                break;
//    //            default:
//    //                break;
//    //        }
//    //    };
//    //    container.GestureRecognizers.Add(pinchRecognizer);
//    //}
//}
