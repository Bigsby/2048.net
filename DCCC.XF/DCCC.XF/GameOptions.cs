using System;
using Xamarin.Forms;

namespace DCCC.XF
{
    public class GameOptions : StackLayout
    {
        public GameOptions(GameOption option, params GameOption[] options)
        {
            Children.Add(BuildOptionButton(option));
            foreach (var op in options)
                Children.Add(BuildOptionButton(op));
        }

        private Button BuildOptionButton(GameOption option)
        {
            var result = new Button();
            result.Text = option.Caption;
            result.Clicked += (s, e) => option.Callback();
            return result;
        }
    }

    public class GameOption
    {
        public GameOption(string caption, Action callback)
        {
            Caption = caption;
            Callback = callback;
        }
        public string Caption { get; private set; }
        public Action Callback { get; private set; }
    }
}
