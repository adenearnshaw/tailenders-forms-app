using GalaSoft.MvvmLight;
using Tailenders.Navigation;

namespace Tailenders.ViewModels
{
    public class BaseViewModel : ViewModelBase, INavigationAware
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public virtual void OnNavigatedTo(object navigationParams)
        {
        }

        public virtual void OnNavigatingFrom()
        {
        }
    }
}
