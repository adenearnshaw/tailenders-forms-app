using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Crashes;
using Tailenders.Common;
using Tailenders.Managers;
using Tailenders.Navigation;
using TailendersApi.Contracts;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private readonly IProfileManager _profileManager;
        private readonly INavigationService _navigationService;

        public ProfilePageViewModel(IProfileManager profileManager, INavigationService navigationService)
        {
            _profileManager = profileManager;
            _navigationService = navigationService;

            Positions = new ObservableCollection<EnumPickerOption>(EnumHelper<CricketPosition>.GetValues(CricketPosition.Cover)
                                                                  .Select(v => new EnumPickerOption((int)v, EnumHelper<CricketPosition>.GetDisplayValue(v))));
            SelectedPosition = Positions.FirstOrDefault();

            Genders = new ObservableCollection<EnumPickerOption>(EnumHelper<Gender>.GetValues(Gender.Male)
                                                                  .Select(v => new EnumPickerOption((int)v, EnumHelper<Gender>.GetDisplayValue(v))));
            SelectedGender = Genders.FirstOrDefault();


            SaveChangesCommand = new RelayCommand(async () => await SaveChanges());
            EditPictureCommand = new RelayCommand(async () => await EditPicture());
            NavigateToDeleteProfileCommand = new RelayCommand(NavigateToDeleteProfile);
            ResetPasswordCommand = new RelayCommand(async () => await ResetPassword());
        }

        private ObservableCollection<EnumPickerOption> _genders;
        public ObservableCollection<EnumPickerOption> Genders
        {
            get => _genders;
            set => Set(ref _genders, value);
        }


        private ObservableCollection<EnumPickerOption> _positions;
        public ObservableCollection<EnumPickerOption> Positions
        {
            get => _positions;
            set => Set(ref _positions, value);
        }

        private EnumPickerOption _selectedPosition;
        public EnumPickerOption SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                Set(ref _selectedPosition, value);
                HasUnsavedChanges = true;
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                Set(ref _name, value);
                HasUnsavedChanges = true;
            }
        }

        private bool? _isNameValid = null;
        public bool? IsNameValid
        {
            get => _isNameValid;
            set
            {
                Set(ref _isNameValid, value);
                RaisePropertyChanged(nameof(IsNameValid));
            }
        }

        private string _age;
        public string Age
        {
            get => _age;
            set
            {
                Set(ref _age, value);
                HasUnsavedChanges = true;
            }
        }

        private bool? _isAgeValid = null;
        public bool? IsAgeValid
        {
            get => _isAgeValid;
            set
            {
                Set(ref _isAgeValid, value);
                RaisePropertyChanged(nameof(IsAgeValid));
            }
        }

        private bool _showAge = true;
        public bool ShowAge
        {
            get => _showAge;
            set
            {
                Set(ref _showAge, value);
                HasUnsavedChanges = true;
            }
        }

        private string _bio;
        public string Bio
        {
            get => _bio;
            set
            {
                Set(ref _bio, value);
                HasUnsavedChanges = true;
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set => Set(ref _location, value);
        }

        private EnumPickerOption _selectedGender;
        public EnumPickerOption SelectedGender
        {
            get => _selectedGender;
            set => Set(ref _selectedGender, value);
        }

        private string _profilePic;
        public string ProfilePic
        {
            get => _profilePic;
            set
            {
                Set(ref _profilePic, value);
                HasUnsavedChanges = true;
            }
        }

        private string _contactDetails;
        public string ContactDetails
        {
            get => _contactDetails;
            set
            {
                Set(ref _contactDetails, value);
                HasUnsavedChanges = true;
            }
        }

        private bool _hasUnsavedChanges;
        public bool HasUnsavedChanges
        {
            get => _hasUnsavedChanges;
            set => Set(ref _hasUnsavedChanges, value);
        }

        private bool _isSavingInProgress;
        public bool IsSavingInProgress
        {
            get => _isSavingInProgress;
            set => Set(ref _isSavingInProgress, value);
        }

        public ICommand SaveChangesCommand { get; private set; }
        public ICommand EditPictureCommand { get; internal set; }
        public ICommand NavigateToDeleteProfileCommand { get; private set; }
        public ICommand ResetPasswordCommand { get; private set; }

        public override void OnNavigatedTo(object navigationParams)
        {
            base.OnNavigatedTo(navigationParams);

            LoadProfile();
        }

        public override void OnNavigatingFrom()
        {
            base.OnNavigatingFrom();

            //SaveChanges();
        }

        private async Task LoadProfile()
        {
            IsBusy = true;

            var profile = await _profileManager.GetUserProfile();

            Name = profile.Name;
            Bio = profile.Bio;
            ProfilePic = profile.Images.OrderByDescending(i => i.UpdatedAt).FirstOrDefault()?.ImageUrl ?? string.Empty;
            Age = profile.Age.ToString();
            ShowAge = profile.ShowAge;
            Location = profile.Location;
            SelectedGender = Genders.FirstOrDefault(c => c.Value == profile.Gender);
            SelectedPosition = Positions.FirstOrDefault(p => p.Value == profile.FavouritePosition);
            ContactDetails = profile.ContactDetails;
            HasUnsavedChanges = false;
            IsBusy = false;
        }

        private async Task SaveChanges()
        {
            IsSavingInProgress = true;
            try
            {
                var profile = await _profileManager.GetUserProfile();

                int.TryParse(Age, out int age);

                profile.Name = Name;
                profile.Age = age;
                profile.ShowAge = ShowAge;
                profile.Location = Location;
                profile.Bio = Bio;
                profile.FavouritePosition = SelectedPosition?.Value ?? 0;
                profile.Gender = SelectedGender?.Value ?? 0;
                profile.ContactDetails = ContactDetails;
                profile.UpdatedAt = DateTime.UtcNow;

                await Task.WhenAll(new List<Task>
                {
                    _profileManager.SaveUserProfile(profile),
                    Task.Delay(500)
                });

                HasUnsavedChanges = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Crashes.TrackError(e);
            }
            finally
            {
                IsSavingInProgress = false;
            }
        }

        private async Task EditPicture()
        {
            if (!CrossMedia.IsSupported)
                return;

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                MessagingCenter.Send(this, MessageNames.NoPickPhotoSupport);
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            try
            {
                IsBusy = true;
                var updatedProfile = await _profileManager.UploadProfileImage(file);
                ProfilePic = updatedProfile.Images.First().ImageUrl;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void NavigateToDeleteProfile()
        {
            _navigationService.NavigateTo(PageKeys.DeleteProfilePage);
        }

        private async Task ResetPassword()
        {
            await _profileManager.RequestPasswordReset();
        }
    }
}
