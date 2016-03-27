using Xamarin.Forms;

namespace DCCC.XF
{
    public class GameCell : Grid
    {
        private readonly Label _label;
        public GameCell()
        {
            Padding = 5;
            BackgroundColor = Color.Gray;
            HeightRequest = 100;
            WidthRequest = 100;
            _label = new Label
            {
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
                FontSize = GetFontSize(0),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            _label.PropertyChanged += (s, e) => {
                var l = s as Label;
                if (null == s) return;

                if (null != l && e.PropertyName == "Text")
                    l.FontSize = GetFontSize(l.Text?.Length);
            };

            Children.Add(_label);
        }

        private double GetFontSize(int? digits)
        {
            switch (digits)
            {
                case 0: return 0;
                case 1: return 40;
                case 2: return 35;
                case 3: return 30;
                case 4: return 25;
                case 5: return 20;
                default:
                    return 20;
            }
        }

        public string Text
        {
            get { return _label.Text; }
            set { _label.Text = value; }
        }
    }
}
