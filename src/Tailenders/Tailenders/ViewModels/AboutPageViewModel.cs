using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Essentials;

namespace Tailenders.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {
        private const string TermsUrl = "http://midwicketapp.a10w.com/terms_and_conditions.html";
        private const string PrivacyUrl = "http://midwicketapp.a10w.com/privacy_policy.html";
        private const string AcknowledgementsUrl = "http://midwicketapp.a10w.com/acknowledgements.html";

        public AboutPageViewModel()
        {
            SendFeedbackCommand = new RelayCommand(async () => await SendFeeback());
            ShowTermsCommand = new RelayCommand(async () => await ShowTerms());
            ShowPrivacyCommand = new RelayCommand(async () => await ShowPrivacy());
            ShowAcknowledgementsCommand = new RelayCommand(async () => await ShowAcknowledgements());
        }

        private string _appVersion;
        public string AppVersion
        {
            get => _appVersion;
            set => Set(ref _appVersion, value);
        }

        public ICommand SendFeedbackCommand { get; }
        public ICommand ShowTermsCommand { get; }
        public ICommand ShowPrivacyCommand { get; }
        public ICommand ShowAcknowledgementsCommand { get; }

        public override void OnNavigatedTo(object navigationParams)
        {
            base.OnNavigatedTo(navigationParams);

            var appVersion = AppInfo.VersionString;
#if DEBUG
            appVersion += $".{AppInfo.BuildString}";
#endif
            AppVersion = $"v{appVersion}";

        }

        private async Task SendFeeback()
        {
            try
            {
                await Email.ComposeAsync("App feedback", string.Empty, "midwicketapp@a10w.com");
            }
            catch (Exception e)
            {

            }
        }

        private async Task ShowTerms()
        {
            await ShowUrl(TermsUrl);
        }

        private async Task ShowPrivacy()
        {
            await ShowUrl(PrivacyUrl);
        }

        private async Task ShowAcknowledgements()
        {
            await ShowUrl(AcknowledgementsUrl);
        }
    }
}
