using Xamarin.Forms;

namespace Tailenders.Views
{
    public partial class LaunchPage : ContentPage
    {
        public LaunchPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Cancel");
        }
    }
}
