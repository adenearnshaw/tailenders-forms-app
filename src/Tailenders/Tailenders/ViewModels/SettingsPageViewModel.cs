
namespace Tailenders.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public SettingsPageViewModel()
        {
        }

        private int _minAge = 21;
        public int MinAge
        {
            get => _minAge;
            set => Set(ref _minAge, value);
        }

        private int _maxAge = 50;
        public int MaxAge
        {
            get => _maxAge;
            set => Set(ref _maxAge, value);
        }
    }
}
