using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
	}
}