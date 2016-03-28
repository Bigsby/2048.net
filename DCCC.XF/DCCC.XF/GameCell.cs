using Xamarin.Forms;

namespace DCCC.XF
{
    public class GameCell : Grid
    {
        private readonly Label _label;
        private readonly double _fontInitialSize;
        public GameCell(double size)
        {
            _fontInitialSize = size / 2;
            BackgroundColor = Color.Gray;
            WidthRequest = HeightRequest = size;
            _label = new Label
            {
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
                FontSize = GetFontSize(string.Empty),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            Children.Add(_label);
        }

        public string Text
        {
            get { return _label.Text; }
            set
            {
                _label.Text = value;
                _label.FontSize = GetFontSize(value);
            }
        }

        private double GetFontSize(string text)
        {
            if (text?.Length < 3)
                return _fontInitialSize;

            return _fontInitialSize * 2 / text.Length;
        }
    }
}
