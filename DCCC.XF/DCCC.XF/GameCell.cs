using System;
using Xamarin.Forms;

namespace DCCC.XF
{
    public class GameCell : Grid
    {
        private readonly Label _label;
        private readonly double _fontInitialSize;
        private uint _value;

        public GameCell(double size)
        {
            _fontInitialSize = size / 2;
            BackgroundColor = GameColors.GetTileBackgroundColor(0);

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
            private set
            {
                _label.Text = value;
                _label.FontSize = GetFontSize(value);
            }
        }

        public uint Value
        {
            get { return _value; }
            set
            {
                if (value == _value) return;
                _value = value;
                Text = _value == 0 ? string.Empty : _value.ToString();
                SetColors(_value);
            }
        }

        private void SetColors(uint value)
        {
            var index = value == 0 ? 0 : (int)Math.Log(value, 2);
            BackgroundColor = GameColors.GetTileBackgroundColor(index);
            _label.TextColor = GameColors.GetTileColor(index);
        }

        private double GetFontSize(string text)
        {
            if (text?.Length < 3)
                return _fontInitialSize;

            return _fontInitialSize * 2 / text.Length;
        }
    }
}
