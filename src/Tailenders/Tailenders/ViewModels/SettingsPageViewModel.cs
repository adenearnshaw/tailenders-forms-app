
namespace Tailenders.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public SettingsPageViewModel()
        {
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

        private string _searchFor = "Men";
        public string SearchFor
        {
            get => _searchFor;
            set => Set(ref _searchFor, value);
        }
    }
}
