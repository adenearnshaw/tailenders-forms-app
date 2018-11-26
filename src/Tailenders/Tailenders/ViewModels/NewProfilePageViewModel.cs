using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Tailenders.Common;
using Tailenders.Managers;
using Tailenders.Navigation;
using TailendersApi.Client;
using TailendersApi.Contracts;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class NewProfilePageViewModel : BaseViewModel
    {
        private readonly ICredentialsProvider _credentialsProvider;
        private readonly IProfileManager _profileManager;
        private readonly INavigationService _navigationService;

        private MediaFile _profilePhotoFile;

        public NewProfilePageViewModel(ICredentialsProvider credentialsProvider,
                                       IProfileManager profileManager,
                                       INavigationService navigationService)
        {
            _credentialsProvider = credentialsProvider;
            _profileManager = profileManager;
            _navigationService = navigationService;

            ProfileVm = new ProfilePageViewModel(profileManager);
            SettingsVm = new SettingsPageViewModel();

            ProfileVm.EditPictureCommand = new RelayCommand(async () => await SelectPicture());
            CreateProfileCommand = new RelayCommand(async () => await CreateProfile());
        }

        public ProfilePageViewModel ProfileVm { get; }
        public SettingsPageViewModel SettingsVm { get; }

        public ICommand CreateProfileCommand { get; private set; }

        private async Task CreateProfile()
        {
            if (!IsFormValid())
                return;

            int.TryParse(ProfileVm.Age, out int age);
            var minAge = Convert.ToInt32(SettingsVm.MinAge);
            var maxAge = Convert.ToInt32(SettingsVm.MaxAge);

            var profile = new Profile
            {
                Id = _credentialsProvider.UserId,
                Name = ProfileVm.Name,
                Age = age,
                ShowAge = ProfileVm.ShowAge,
                Location = ProfileVm.Location,
                Bio = ProfileVm.Bio,
                FavouritePosition = ProfileVm.SelectedPosition?.Value ?? 0,
                SearchShowInCategory = ProfileVm.SearchShowIn?.Value ?? 0,
                SearchForCategory = SettingsVm.SearchFor?.Value ?? 0,
                SearchRadius = 30,
                SearchMinAge = minAge,
                SearchMaxAge = maxAge,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return;

            await _profileManager.SaveUserProfile(profile, true);
            await _profileManager.UploadProfileImage(_profilePhotoFile);

            _navigationService.NavigateTo(PageKeys.HomePage, historyBehavior: NavigationHistoryBehavior.ClearHistory);
        }

        private bool IsFormValid()
        {
            ProfileVm.IsNameValid = !string.IsNullOrWhiteSpace(ProfileVm.Name);

            return ProfileVm.IsNameValid;
        }

        private async Task SelectPicture()
        {
            if (!CrossMedia.IsSupported)
                return;

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                MessagingCenter.Send(this, MessageNames.NoPickPhotoSupport);
                return;
            }

            _profilePhotoFile = await CrossMedia.Current.PickPhotoAsync();
            ProfileVm.ProfilePic = _profilePhotoFile.Path;
        }
    }
}
