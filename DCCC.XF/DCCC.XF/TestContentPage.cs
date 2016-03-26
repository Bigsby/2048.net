using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace DCCC.XF
{
    public class TestContentPage : ContentPage
    {
        public TestContentPage(Action go)
        {
            var button = new Button { Text = "Click" };
            button.Clicked += (s, e) => go();

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" },
                    new TestContentView(),
                    button
                }
            };
        }
    }
}
