using System.Collections.ObjectModel;
using System.Linq;
using Tailenders.Common;
using TailendersApi.Contracts;

namespace Tailenders.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public SettingsPageViewModel()
        {
            SearchCategories = new ObservableCollection<EnumPickerOption>(EnumHelper<SearchCategory>.GetValues(SearchCategory.Men)
                                                                  .Select(v => new EnumPickerOption((int)v, EnumHelper<SearchCategory>.GetDisplayValue(v))));
            SearchFor = SearchCategories.FirstOrDefault();
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
            set => Set(ref _minAge, value);
        }

        private float _maxAge = 50f;
        public float MaxAge
        {
            get => _maxAge;
            set => Set(ref _maxAge, value);
        }

        private double _searchRadius = 0.5;
        public double SearchRadius
        {
            get => _searchRadius;
            set => Set(ref _searchRadius, value);
        }

        private EnumPickerOption _searchFor;
        public EnumPickerOption SearchFor
        {
            get => _searchFor;
            set => Set(ref _searchFor, value);
        }
    }
}
