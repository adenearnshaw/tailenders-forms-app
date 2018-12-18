using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.AppCenter.Crashes;
using Tailenders.Managers;
using Tailenders.Navigation;
using Tailenders.Services;
using Tailenders.Views;
using TailendersApi.Client.Exceptions;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            TryLoginCommand = new RelayCommand(async () => await TryLogin());
            ShowTermsCommand = new RelayCommand(async () => await ShowTerms());
            EnableDevModeCommand = new RelayCommand(EnableDevMode);
        }

        public ICommand TryLoginCommand { get; }
        public ICommand ShowTermsCommand { get; }
        public ICommand EnableDevModeCommand { get; }

        public async Task TryLogin()
        {
            IsBusy = true;

            var didLogin = await AuthenticationService.Instance.TryLogin();

            if (didLogin)
            {
                try
                {
                    var profileManager = SimpleIoc.Default.GetInstance<IProfileManager>();
                    await profileManager.GetUserProfile();
                    Application.Current.MainPage = App.CreateNavigationPage(new MasterPage());
                }
                catch (ProfileBlockedException)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new BlockedProfilePage());
                }
                catch (ProfileDoesntExistException)
                {
                    Application.Current.MainPage = App.CreateNavigationPage(new NewProfilePage());
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    await Application.Current.MainPage.Navigation.PushAsync(new ErrorPage());
                }
            }
            
            IsBusy = false;
        }

        private async Task ShowTerms()
        {
            await ShowUrl(TermsUrl);
        }

        private void EnableDevMode()
        {
            // TODO
        }
    }
}