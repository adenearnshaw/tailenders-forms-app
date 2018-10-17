using System;
using Xamarin.Forms;
// using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Tailenders.Views
{
    public partial class MainPage : ContentPage
    {
        public event EventHandler MenuClicked;

        public MainPage()
        {
            InitializeComponent();
            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        private void MenuButtonClicked(object sender, EventArgs e)
        {
            MenuClicked?.Invoke(this, e);
        }
    }
}
