using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace DCCC.XF
{
    public class GridTest : ContentPage
    {
        Grid _grid;
        public GridTest()
        {
            Content = _grid = new Grid();
            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            _grid.BackgroundColor = Color.Gray;
            _grid.RowSpacing = 0;

            var header = new Grid
            {
                BackgroundColor = Color.Blue

            };
            header.Children.Add(new Label { Text = "Header", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center });
            _grid.Children.Add(header);

            var content = new Grid
            {
                BackgroundColor = Color.Yellow
            };
            _grid.Children.Add(content);
            Grid.SetRow(content, 1);

            var footer = new Grid
            {
                BackgroundColor = Color.Pink

            };
            footer.Children.Add(new Label { Text = "Footer", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center });
            _grid.Children.Add(footer);
            Grid.SetRow(footer, 2);
        }
    }
}
