using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace DCCC.XF
{
    public class TestContentView : ContentView
    {
        public TestContentView()
        {
            Content = new Label { Text = "Hello ContentView" };
        }
    }
}
