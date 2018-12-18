using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Crashes;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Tailenders.Common;
using Tailenders.Managers;
using Tailenders.Navigation;
using Tailenders.Views;
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

            ProfileVm = new ProfilePageViewModel(profileManager, navigationService);
            SettingsVm = new SettingsPageViewModel(profileManager);
            AboutVm = new AboutPageViewModel();

            ProfileVm.EditPictureCommand = new RelayCommand(async () => await SelectPicture());
            CreateProfileCommand = new RelayCommand(async () => await CreateProfile());
        }

        public ProfilePageViewModel ProfileVm { get; }
        public SettingsPageViewModel SettingsVm { get; }
        public AboutPageViewModel AboutVm { get; }

        private bool _canCreateProfile = false;
        public bool CanCreateProfile
        {
            get => _canCreateProfile;
            set => Set(ref _canCreateProfile, value);
        }

        private bool _hasAgreedToTerms = false;
        public bool HasAgreedToTerms
        {
            get => _hasAgreedToTerms;
            set
            {
                Set(ref _hasAgreedToTerms, value);
                CanCreateProfile = IsFormValid();
            }
        }


        public ICommand CreateProfileCommand { get; private set; }

        private async Task CreateProfile()
        {
            try
            {
                IsBusy = true;

                int.TryParse(ProfileVm.Age, out int age);

                if (!string.IsNullOrWhiteSpace(ProfileVm.Age) && age < 18)
                {
                    MessagingCenter.Send(this, MessageNames.NotOldEnough);
                }

                if (!IsFormValid(age))
                    return;


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
                    Gender = ProfileVm.SelectedGender?.Value ?? 0,
                    SearchForCategory = SettingsVm.SearchFor?.Value ?? 0,
                    SearchRadius = 30,
                    SearchMinAge = minAge,
                    SearchMaxAge = maxAge,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _profileManager.SaveUserProfile(profile, true);
                await _profileManager.UploadProfileImage(_profilePhotoFile);

                Application.Current.MainPage = App.CreateNavigationPage(new MasterPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool IsFormValid(int age = 0)
        {
            if (age == 0)
                int.TryParse(ProfileVm.Age, out age);

            ProfileVm.IsNameValid = !string.IsNullOrWhiteSpace(ProfileVm.Name);
            ProfileVm.IsAgeValid = age >= 18;

            return ProfileVm.IsNameValid.Value
                && ProfileVm.IsAgeValid.Value
                && HasAgreedToTerms;
        }

        private async Task SelectPicture()
        {
            if (!CrossMedia.IsSupported)
                return;

            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                _profilePhotoFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen,
                    CompressionQuality = 75
                });
                ProfileVm.ProfilePic = _profilePhotoFile.Path;
            }
            else
            {
                MessagingCenter.Send(this, MessageNames.NoPickPhotoSupport);
            }
        }
    }
}
