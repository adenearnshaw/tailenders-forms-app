using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
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
        public static double ScreenHeight;
        public static double ScreenWidth;

        protected override void OnStart()
        {
            base.OnStart();

            AppCenter.Start("ios=d2272fc1-1aea-46a7-b426-6a6a4124bbee;", typeof(Analytics), typeof(Crashes));
        }
    }
}
