using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tailenders.Managers;
using Tailenders.Navigation;

namespace Tailenders.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IProfileManager _profileManager;

        public MenuPageViewModel(INavigationService navigationService,
                                 IProfileManager profileManager)
        {
            _navigationService = navigationService;
            _profileManager = profileManager;

            ShowPodcastPageCommand = new RelayCommand(() => _navigationService.NavigateTo(PageKeys.PodcastPage));
            ShowProfilePageCommand = new RelayCommand(() => _navigationService.NavigateTo(PageKeys.ProfilePage));
            ShowSettingsPageCommand = new RelayCommand(() => _navigationService.NavigateTo(PageKeys.SearchSettingsPage));
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _profilePic;
        public string ProfilePic
        {
            get => _profilePic;
            set => Set(ref _profilePic, value);
        }

        public ICommand ShowProfilePageCommand { get; }
        public ICommand ShowSettingsPageCommand { get; }
        public ICommand ShowPodcastPageCommand { get; }

        public override void OnNavigatedTo(object navigationParams)
        {
            base.OnNavigatedTo(navigationParams);
            LoadProfile();
        }

        private async Task LoadProfile()
        {
            var profile = await _profileManager.GetUserProfile();
            Name = profile.Name;
            ProfilePic = profile.Images.FirstOrDefault()?.ImageUrl ?? string.Empty;
        }
    }
}
