﻿using System;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Tailenders.Common;
using Tailenders.Managers;
using TailendersApi.Contracts;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private readonly IProfileManager _profileManager;

        public ProfilePageViewModel(IProfileManager profileManager)
        {
            _profileManager = profileManager;

            Positions = new ObservableCollection<EnumPickerOption>(EnumHelper<CricketPosition>.GetValues(CricketPosition.Cover)
                                                                  .Select(v => new EnumPickerOption((int)v, EnumHelper<CricketPosition>.GetDisplayValue(v))));
            SelectedPosition = Positions.FirstOrDefault();

            Genders = new ObservableCollection<EnumPickerOption>(EnumHelper<SearchCategory>.GetValues(Gender.Male)
                                                                  .Select(v => new EnumPickerOption((int)v, EnumHelper<SearchCategory>.GetDisplayValue(v))));
            SelectedGender = Genders.FirstOrDefault();


            SaveChangesCommand = new RelayCommand(async () => await SaveChanges());
            EditPictureCommand = new RelayCommand(async () => await EditPicture());
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

        private bool _hasUnsavedChanges;
        public bool HasUnsavedChanges
        {
            get => _hasUnsavedChanges;
            set => Set(ref _hasUnsavedChanges, value);
        }

        public ICommand SaveChangesCommand { get; private set; }
        public ICommand EditPictureCommand { get; internal set; }

        public override void OnNavigatedTo(object navigationParams)
        {
            base.OnNavigatedTo(navigationParams);

            LoadProfile();
        }

        public override void OnNavigatingFrom()
        {
            base.OnNavigatingFrom();

            SaveChanges();
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

            IsBusy = false;
        }

        private async Task SaveChanges()
        {
            //TODO await ProfileManager.UpdateProfile(updatedProfile);
            HasUnsavedChanges = false;
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

    }
}
