﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DCCC.XF
{
    public partial class TestPage : ContentPage
    {
        public TestPage(Action go)
        {
            InitializeComponent();
            button.Clicked += (s, e) => go();
        }
    }
}
