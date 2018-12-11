using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tailenders.Managers;
using Tailenders.Navigation;
using Tailenders.Services;
using Xamarin.Essentials;

namespace Tailenders.ViewModels
{
    public class DeleteProfilePageViewModel : BaseViewModel
    {
        private readonly IProfileManager _profileManager;
        private readonly INavigationService _navigationService;

        public DeleteProfilePageViewModel(IProfileManager profileManager, INavigationService navigationService)
        {
            _profileManager = profileManager;
            _navigationService = navigationService;

            DeleteProfileCommand = new RelayCommand(async () => await DeleteProfile());
            SendEmailCommand = new RelayCommand(async () => await SendEmail());
        }

        public ICommand DeleteProfileCommand { get; }
        public ICommand SendEmailCommand { get; }

        private async Task DeleteProfile()
        {
            await _profileManager.DeleteUserProfile();
            await AuthenticationService.Instance.TryLogout();
            _navigationService.NavigateTo(PageKeys.LoginPage, null, NavigationHistoryBehavior.ClearHistory);
        }

        private async Task SendEmail()
        {
            try
            {
                await Email.ComposeAsync("Account deletion", string.Empty, "midwicketapp@a10w.com");
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
