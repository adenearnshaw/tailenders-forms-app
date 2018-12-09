using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.AppCenter.Crashes;
using Plugin.Iconize;
using Tailenders.Managers;
using Tailenders.Services;
using Tailenders.Views;
using TailendersApi.Client.Exceptions;
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

            var isLoginSuccessful = await AuthenticationService.Instance.TrySilentLogin();


            if (isLoginSuccessful)
            {
                try
                {
                    var profileManager = SimpleIoc.Default.GetInstance<IProfileManager>();
                    await profileManager.GetUserProfile();
                    Application.Current.MainPage = App.CreateNavigationPage(new MasterPage());
                }
                catch (ProfileDoesntExistException)
                {
                    Application.Current.MainPage = App.CreateNavigationPage(new LoginPage());
                    await Application.Current.MainPage.Navigation.PushAsync(new NewProfilePage());
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    Application.Current.MainPage = App.CreateNavigationPage(new ErrorPage());
                }
            }
            else
            {
                Application.Current.MainPage = new LoginPage();
            }

            IsBusy = false;
        }

        
    }
}
