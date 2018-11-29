using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Plugin.Iconize;
using Tailenders.Managers;
using Tailenders.Managers.Exceptions;
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

            var profileManager = SimpleIoc.Default.GetInstance<IProfileManager>();
            var token = await AuthenticationService.Instance.TryLogin();
            try
            {
                var userProfile = await profileManager.GetUserProfile();
                Application.Current.MainPage = CreateNavigationPage(new MasterPage());
            }
            catch (UserDoesntExistException)
            {
                Application.Current.MainPage = CreateNavigationPage(new NewProfilePage());
            }
            catch (Exception ex)
            {
                Application.Current.MainPage = CreateNavigationPage(new ErrorPage());
            }

            IsBusy = false;
        }

        private IconNavigationPage CreateNavigationPage(Page basePage)
        {
            return new IconNavigationPage(basePage)
            {
                BarBackgroundColor = Color.FromHex("#8AAF5F"),
                BarTextColor = Color.Snow
            };
        }
    }
}
