using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Tailenders.Navigation;
using Xamarin.Essentials;

namespace Tailenders.ViewModels
{
    public class BaseViewModel : ViewModelBase, INavigationAware
    {
        //TODO Extract to consts file
        protected const string TermsUrl = "http://midwicketapp.a10w.com/terms_and_conditions.html";
        protected const string PrivacyUrl = "http://midwicketapp.a10w.com/privacy_policy.html";
        protected const string AcknowledgementsUrl = "http://midwicketapp.a10w.com/acknowledgements.html";
        protected const string ContactEmail = "midwicketapp.a10w.com";

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
