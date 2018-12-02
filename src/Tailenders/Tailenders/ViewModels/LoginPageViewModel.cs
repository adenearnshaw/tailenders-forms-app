using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Plugin.Iconize;
using Tailenders.Managers;
using Tailenders.Managers.Exceptions;
using Tailenders.Navigation;
using Tailenders.Services;
using Tailenders.Views;
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
        }

        public ICommand TryLoginCommand { get; }

        public async Task TryLogin()
        {
            IsBusy = true;

            var profileManager = SimpleIoc.Default.GetInstance<IProfileManager>();
            await AuthenticationService.Instance.TryLogin();
            try
            {
                await profileManager.GetUserProfile();
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