using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tailenders.Views
{
    public partial class LaunchPage : ContentPage
    {
        public LaunchPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
