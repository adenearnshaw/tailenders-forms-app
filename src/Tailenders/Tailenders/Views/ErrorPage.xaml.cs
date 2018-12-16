using Xamarin.Forms;
using System;
using Xamarin.Essentials;
using Tailenders.Services;

namespace Tailenders.Views
{
    public partial class ErrorPage : ContentPage
    {
        public ErrorPage()
        {
            InitializeComponent();
            AuthenticationService.Instance.TryLogout();
        }

        public async void SendFeedbackClicked(object sender, EventArgs e)
        {
            try
            {
                var message = new EmailMessage("App feedback", "", "midwicketapp@a10w.com" );
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException)
            {
                // Email is not supported on this device
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Email not supported", "To send feedback, please add an email account", "Ok");
                });
            }
            catch (Exception)
            {
                // Some other exception occurred
            }
        }
    }
}
