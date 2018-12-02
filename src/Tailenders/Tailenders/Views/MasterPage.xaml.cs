using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailenders.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            mainPage.MenuClicked += MainPageOnMenuClicked;
            menuPage.LogoutClicked += MenuPageOnLogoutClicked;
        }

        protected override void OnDisappearing()
        {
            mainPage.MenuClicked -= MainPageOnMenuClicked;
            menuPage.LogoutClicked -= MenuPageOnLogoutClicked;
            base.OnDisappearing();
        }

        private void MenuPageOnLogoutClicked(object sender, EventArgs e)
        {
            this.IsPresented = false;
            this.mainPage.SetIsBusyOverlay(true, "Logging out");
        }

        private void MainPageOnMenuClicked(object sender, EventArgs e)
        {
            this.IsPresented = !this.IsPresented;
        }
    }
}