using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailenders.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public event EventHandler LogoutClicked;

        public MenuPage ()
        {
            InitializeComponent ();
        }

        private void LogoutButtonClicked(object sender, EventArgs e)
        {
            LogoutClicked?.Invoke(this, e);
        }
    }
}