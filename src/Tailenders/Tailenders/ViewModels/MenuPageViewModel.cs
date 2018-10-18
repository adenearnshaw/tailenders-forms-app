using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tailenders.Navigation;

namespace Tailenders.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public MenuPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowPodcastPageCommand = new RelayCommand(() => _navigationService.NavigateTo(PageKeys.PodcastPage));
            ShowProfilePageCommand = new RelayCommand(() => _navigationService.NavigateTo(PageKeys.ProfilePage));
            ShowSettingsPageCommand = new RelayCommand(() => _navigationService.NavigateTo(PageKeys.SearchSettingsPage));
        }

        public ICommand ShowProfilePageCommand { get; }
        public ICommand ShowSettingsPageCommand { get; }
        public ICommand ShowPodcastPageCommand { get; }
    }
}
