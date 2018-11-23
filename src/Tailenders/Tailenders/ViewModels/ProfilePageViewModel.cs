using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
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

            SearchCategories = new ObservableCollection<EnumPickerOption>(EnumHelper<SearchCategory>.GetValues(SearchCategory.Men)
                                                                  .Select(v => new EnumPickerOption((int)v, EnumHelper<SearchCategory>.GetDisplayValue(v))));

            SaveChangesCommand = new RelayCommand(async () => await SaveChanges());
            EditPictureCommand = new RelayCommand(async () => await EditPicture());
        }

        private ObservableCollection<EnumPickerOption> _searchCategories;
        public ObservableCollection<EnumPickerOption> SearchCategories
        {
            get => _searchCategories;
            set => Set(ref _searchCategories, value);
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

        private bool _showAge;
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

        private EnumPickerOption _searchShowIn;
        public EnumPickerOption SearchShowIn
        {
            get => _searchShowIn;
            set => Set(ref _searchShowIn, value);
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

        public ICommand SaveChangesCommand { get; set; }
        public ICommand EditPictureCommand { get; set; }

        public override void OnNavigatedTo(object navigationParams)
        {
            base.OnNavigatedTo(navigationParams);

            //Name = "Aden";
            //Location = "Preston";
            //Age = "31";
            //ShowAge = true;
            //SelectedPosition = "Long stop";
            //Bio = "Ranked number 11 in the world for most dropped catches at my local school.";
            //ProfilePic = "Tile5.png";
            //SearchShowIn = "Men";
            //HasUnsavedChanges = false;
        }

        public override void OnNavigatingFrom()
        {
            base.OnNavigatingFrom();

            SaveChanges();
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

            IsBusy = true;
            var updatedProfile = await _profileManager.UploadProfileImage(file);
            ProfilePic = updatedProfile.Images.First().ImageUrl;
            IsBusy = false;
        }

    }
}
