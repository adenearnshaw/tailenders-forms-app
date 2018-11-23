using Xamarin.Forms;
using System;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace Tailenders.Views
{
    public partial class ErrorPage : ContentPage
    {
        public ErrorPage()
        {
            InitializeComponent();
        }

        public async void SendFeedbackClicked(object sender, EventArgs e)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "App feedback",
                    Body = "",
                    To = new List<string>() { "tailendersapp@a10w.com" },
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Email not supported", "To send feedback, please add an email account", "Ok");
                });
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }
    }
}
