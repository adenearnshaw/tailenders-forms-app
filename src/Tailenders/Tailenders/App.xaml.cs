using Microsoft.Identity.Client;
using Plugin.Iconize;
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
            Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());

            MainPage = new LaunchPage();
        }

        //public static ViewModelLocator Locator { get; set; }
        public static UIParent UiParent = null;
    }
}
