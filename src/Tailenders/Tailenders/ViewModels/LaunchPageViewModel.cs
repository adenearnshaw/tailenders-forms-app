using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Iconize;
using Tailenders.Services;
using Tailenders.Views;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class LaunchPageViewModel : BaseViewModel
    {

        public override void OnNavigatedTo(object navigationParams)
        {
            base.OnNavigatedTo(navigationParams);
            InitiateApp();
        }

        public async Task InitiateApp()
        {
            IsBusy = true;

            //App.Locator = new ViewModelLocator();

            var token = await AuthenticationService.Instance.TryLogin();

            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    //TODO Make a factory method
                    Application.Current.MainPage = new IconNavigationPage(new MasterPage())
                    {
                        BarBackgroundColor = Color.FromHex("#8AAF5F"),
                        BarTextColor = Color.Snow
                    };
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            IsBusy = false;
        }
    }
}
