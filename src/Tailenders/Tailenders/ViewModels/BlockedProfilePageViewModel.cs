using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Essentials;

namespace Tailenders.ViewModels
{
    public class BlockedProfilePageViewModel : BaseViewModel
    {
        private const string TermsUrl = "http://midwicketapp.a10w.com/terms_and_conditions.html#behaviour";

        public BlockedProfilePageViewModel()
        {
            SendFeedbackCommand = new RelayCommand(async () => await SendFeedback());
            ShowTermsCommand = new RelayCommand(async () => await ShowTerms());
        }

        public ICommand SendFeedbackCommand { get; }
        public ICommand ShowTermsCommand { get; }

        private async Task SendFeedback()
        {
            try
            {
                var message = new EmailMessage("Why am I blocked?", "", "midwicketapp@a10w.com");
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException)
            {
                // Email is not supported on this device
            }
            catch (Exception)
            {
                // Some other exception occurred
            }
        }

        private async Task ShowTerms()
        {
            await ShowUrl(TermsUrl);
        }
    }
}
