using DCCC.Interfaces;
using Xamarin.Forms;

namespace DCCC.XF
{
    public class GameOptions : StackLayout
    {
        public GameOptions(double fontSize, string title, params GameOption[] options)
        {
            Spacing = fontSize;
            Children.Add(new Label
            {
                Text = title,
                TextColor = Color.FromHex("5040A6"),
                FontSize = fontSize,
                HorizontalOptions = LayoutOptions.Center
            });

            foreach (var option in options)
                Children.Add(new Button
                {
                    FontSize = fontSize,
                    Text = option.Caption,
                    TextColor = Color.FromHex("4A87E1"),
                    BackgroundColor = Color.FromHex("090D13"),
                    Command = new Command(option.Callback)
                });
        }
    }
}
