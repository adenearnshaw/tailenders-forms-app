using Tailenders.Controls.TransparentNavBar;
using Tailenders.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Tailenders
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Plugin.Iconize.Iconize
                  .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                  .With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
                  .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());

            MainPage = new NavigationPage(new MasterPage())
            {
                BarBackgroundColor = Color.FromHex("#8AAF5F"),
                BarTextColor = Color.Snow
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
