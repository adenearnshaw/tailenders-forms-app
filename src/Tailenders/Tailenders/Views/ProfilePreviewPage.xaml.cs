using System;
using Tailenders.Common;
using Tailenders.ViewModels;
using TailendersApi.Contracts;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Tailenders.Views
{
    public partial class ProfilePreviewPage : ContentPage
    {
        public ProfilePreviewPage(CardItemViewModel cardItem)
        {
            InitializeComponent();
            BindingContext = new ProfilePreviewPageViewModel(cardItem);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Instance.Subscribe<ProfilePreviewPageViewModel>(this, MessageNames.ReportProfile, ShowReportProfileDialog);
            MessagingCenter.Instance.Subscribe<ProfilePreviewPageViewModel>(this, MessageNames.BlockProfile, ShowBlockProfileConfirmation);
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Instance.Unsubscribe<ProfilePreviewPageViewModel>(this, MessageNames.ReportProfile);
            MessagingCenter.Instance.Unsubscribe<ProfilePreviewPageViewModel>(this, MessageNames.BlockProfile);
            base.OnDisappearing();
        }

        public void CloseModalClicked(object sender, EventArgs args)
        {
            App.CloseModal();
        }

        private void ShowBlockProfileConfirmation(ProfilePreviewPageViewModel vm)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Block profile",
                    "Blocking this profile prevents you from ever seeing this pairing again. Are you sure you want to block this profile?", "Block", "Cancel");

                await vm.BlockProfileCallback(result);
                MessagingCenter.Instance.Send(App.Current, MessageNames.ReloadSearch);
                App.CloseModal();
            });
        }

        private void ShowReportProfileDialog(ProfilePreviewPageViewModel vm)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var action = await DisplayActionSheet("Report", "Cancel", null, "Inappropriate profile", "Inappropriate photo", "Feels like spam", "Other");

                var reason = action == "Inappropriate profile" ? ReportProfileReason.InappropriateProfile
                           : action == "Inappropriate photo" ? ReportProfileReason.InappropriatePhotos
                           : action == "Feels like spam" ? ReportProfileReason.Spam
                           : ReportProfileReason.Other;

                await vm.ReportProfileCallback(reason);

                await DisplayAlert("Reported", "Thanks for help, the profile will be reviewed and the appropriate action taken.", "Ok");
            });
        }
    }
}
