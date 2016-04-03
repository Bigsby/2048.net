using DCCC.Interfaces;
using System;
using Xamarin.Forms;

namespace DCCC.XF.GameControls
{
    public class GamePage : ContentPage, IGamePage
    {
        private Grid _mainGrid;
        private GameBoard _gameGrid;
        private GameHeader _gameHeader;
        private double _fontSize;
        private bool _isGameOn;
        private Size _currentSize;

        public GamePage()
        {            
            Appearing += (s, e) =>
            {
                _currentSize = Height == -1 ?
                    App.ScreenSize :
                    new Size(Width, Height);

                _mainGrid = new Grid
                {
                    BackgroundColor = Color.Transparent, // Necessary for PanGestureRecognizer to read pans outside content
                    RowSpacing = 0,
                    ColumnSpacing = 0
                };

                BackgroundColor = Color.Black;

                _gameHeader = new GameHeader(_fontSize = CalculateHeaderFontSize());

                _gameGrid = new GameBoard(CalculateGridSize(), 4);
                _gameGrid.VerticalOptions = _gameGrid.HorizontalOptions = LayoutOptions.Center;

                _mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                _mainGrid.RowDefinitions.Add(new RowDefinition());

                _mainGrid.Children.Add(_gameHeader);
                Grid.SetRow(_gameGrid, 1);
                _mainGrid.Children.Add(_gameGrid);

                if (Device.Idiom == TargetIdiom.Desktop)
                    _mainGrid.Children.Add(BuildKeyboardInputEntry());

                Content = _mainGrid;
                Content.GestureRecognizers.Add(BuildSwipeRecognizer());

                Ready?.Invoke(this, EventArgs.Empty);
            };
        }

        public double CalculatedFontSize { get { return _fontSize; } }
        public event EventHandler<MoveEventArgs> Moved;
        public event EventHandler Ready;

        internal Action ShowOptions(bool hideScore, GameOptions options)
        {
            _isGameOn = false;
            if (hideScore)
                _gameHeader.IsVisible = false;

            _gameGrid.IsVisible = false;

            options.VerticalOptions = LayoutOptions.Center;
            Grid.SetRow(options, 1);
            _mainGrid.Children.Add(options);


            return () => _mainGrid.Children.Remove(options);
        }

        public void Update(IGameState gameState)
        {
            _isGameOn = true;
            _gameGrid.IsVisible = true;
            _gameGrid.Update(gameState.Grid.Cells);
            _gameHeader.Update(gameState.BestScore, gameState.Score);
        }

        internal void ConfirmKeepGoing(Action<bool> resultHandler)
        {
            DisplayAlert("Gaem Won!", "Do you want to keep going?", "Yes", "No").ContinueWith(task =>
                Device.BeginInvokeOnMainThread(() => resultHandler(task.Result)));
        }

        #region Private Methods
        private double CalculateGridSize()
        {
            if (_currentSize.Width > _currentSize.Height)
                return _currentSize.Height * .8;

            return _currentSize.Width * .9;
        }

        private double CalculateHeaderFontSize()
        {
            if (_currentSize.Width > _currentSize.Height)
                return _currentSize.Height * .075;
            return _currentSize.Width * .075;
        }

        private PanGestureRecognizer BuildSwipeRecognizer()
        {
            var panGesture = new PanGestureRecognizer();
            var totalX = .0;
            var totalY = .0;
            panGesture.PanUpdated += (s, e) =>
            {
                switch (e.StatusType)
                {
                    case GestureStatus.Started:
                        break;
                    case GestureStatus.Running:
                        totalX += e.TotalX;
                        totalY += e.TotalY;
                        break;
                    case GestureStatus.Completed:
                        if (Math.Abs(totalX) > Math.Abs(totalY))
                            Move(totalX > 0 ? MoveDirection.Right : MoveDirection.Left);
                        else
                            Move(totalY > 0 ? MoveDirection.Down : MoveDirection.Up);

                        totalX = totalY = 0;
                        break;
                    case GestureStatus.Canceled:
                        break;
                    default:
                        break;
                }
            };

            return panGesture;
        }

        private Entry BuildKeyboardInputEntry()
        {
            var kbInput = new Entry
            {
                Opacity = 0,
                HeightRequest = 1,
                WidthRequest = 1,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start
            };

            kbInput.Unfocused += (s, e) => { if (_isGameOn) kbInput.Focus(); };
            kbInput.TextChanged += (s, e) =>
            {
                var t = e.NewTextValue.ToUpper();
                switch (t)
                {
                    case "A":
                    case "4":
                        Move(MoveDirection.Left);
                        break;
                    case "W":
                    case "8":
                        Move(MoveDirection.Up);
                        break;
                    case "D":
                    case "6":
                        Move(MoveDirection.Right);
                        break;
                    case "S":
                    case "2":
                        Move(MoveDirection.Down);
                        break;
                }

                kbInput.Text = string.Empty;
            };

            kbInput.Focus();
            return kbInput;
        }

        private void Move(MoveDirection direction)
        {
            Moved?.Invoke(this, new MoveEventArgs(direction));
        }
    }
    #endregion
}
