using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using Tailenders.Services;
using Xamarin.Essentials;

namespace Tailenders.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        public void EnableDevTapped(object sender, EventArgs e)
        {
            AuthenticationService.Instance.EnableDevMode();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Dev mode active", "Dev mode now active", "Ok");
            });
        }
    }
}