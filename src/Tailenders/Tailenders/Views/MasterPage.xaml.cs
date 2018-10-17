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

            mainPage.MenuClicked += MainPageOnMenuClicked;
        }

        private void MainPageOnMenuClicked(object sender, EventArgs e)
        {
            this.IsPresented = !this.IsPresented;
        }
    }
}