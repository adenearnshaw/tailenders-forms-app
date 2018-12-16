using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Tailenders.Navigation;
using Xamarin.Essentials;

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

        protected async Task ShowUrl(string uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}
