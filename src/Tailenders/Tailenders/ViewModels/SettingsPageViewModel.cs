using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Crashes;
using Tailenders.Common;
using Tailenders.Managers;
using Tailenders.Views;
using TailendersApi.Contracts;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private readonly IProfileManager _profileManager;

        public SettingsPageViewModel(IProfileManager profileManager)
        {
            _profileManager = profileManager;

            SearchCategories = new ObservableCollection<EnumPickerOption>(EnumHelper<SearchCategory>.GetValues(SearchCategory.Men)
                                                                  .Select(v => new EnumPickerOption((int)v, EnumHelper<SearchCategory>.GetDisplayValue(v))));
            SearchFor = SearchCategories.FirstOrDefault();
            SaveChangesCommand = new RelayCommand(async () => await SaveChanges());
        }

        private ObservableCollection<EnumPickerOption> _searchCategories;
        public ObservableCollection<EnumPickerOption> SearchCategories
        {
            get => _searchCategories;
            set => Set(ref _searchCategories, value);
        }

        private float _minAge = 21f;
        public float MinAge
        {
            get => _minAge;
            set
            {
                if (Set(ref _minAge, value))
                {
                    HasUnsavedChanges = true;
                }
            }
        }

        private float _maxAge = 50f;
        public float MaxAge
        {
            get => _maxAge;
            set
            {
                if(Set(ref _maxAge, value))
                {
                    HasUnsavedChanges = true;
                }
            }
        }

        private double _searchRadius = 0.5;
        public double SearchRadius
        {
            get => _searchRadius;
            set
            {
                if(Set(ref _searchRadius, value))
                {
                    HasUnsavedChanges = true;
                }
            }
        }

        private EnumPickerOption _searchFor;
        public EnumPickerOption SearchFor
        {
            get => _searchFor;
            set
            { 
                if(Set(ref _searchFor, value))
                {
                    HasUnsavedChanges = true;
                }
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

        public override void OnNavigatedTo(object navigationParams)
        {
            base.OnNavigatedTo(navigationParams);

            LoadSettings();
        }

        public override void OnNavigatingFrom()
        {
            MessagingCenter.Instance.Send(App.Current, MessageNames.ReloadSearch);
            base.OnNavigatingFrom();
        }

        private async Task LoadSettings()
        {
            IsBusy = true;

            var profile = await _profileManager.GetUserProfile();

            MinAge = profile.SearchMinAge;
            MaxAge = profile.SearchMaxAge;
            SearchRadius = profile.SearchRadius;
            SearchFor = SearchCategories.FirstOrDefault(c => c.Value == profile.SearchForCategory);

            IsBusy = false;
        }

        private async Task SaveChanges()
        {
            IsSavingInProgress = true;
            try
            {
                var profile = await _profileManager.GetUserProfile();

                profile.SearchMinAge = Convert.ToInt32(MinAge);
                profile.SearchMaxAge = Convert.ToInt32(MaxAge);
                profile.SearchRadius = Convert.ToInt32(SearchRadius);
                profile.SearchForCategory = SearchFor.Value;
                profile.UpdatedAt = DateTime.UtcNow;

                await Task.WhenAll(new List<Task>
                {
                    _profileManager.SaveUserProfile(profile),
                    Task.Delay(500)
                });
                MessagingCenter.Instance.Send(App.Current, MessageNames.ReloadSearch);
                HasUnsavedChanges = false;

            }
            catch (TailendersApi.Client.Exceptions.ProfileBlockedException)
            {
                App.Current.MainPage = App.CreateNavigationPage(new LoginPage());
                await App.Current.MainPage.Navigation.PushAsync(new BlockedProfilePage());
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
    }
}
