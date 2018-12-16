using Tailenders.Common;
using Tailenders.ViewModels;
using TailendersApi.Contracts;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailenders.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchDetailPage : ContentPage
    {
        public MatchDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Instance.Subscribe<MatchDetailPageViewModel>(this, MessageNames.SendContactDetails, ShowContactDetailsConfirmation);
            MessagingCenter.Instance.Subscribe<MatchDetailPageViewModel>(this, MessageNames.Unmatch, ShowUnmatchConfirmation);
            MessagingCenter.Instance.Subscribe<MatchDetailPageViewModel>(this, MessageNames.ReportProfile, ShowReportProfileDialog);
            MessagingCenter.Instance.Subscribe<MatchDetailPageViewModel>(this, MessageNames.BlockProfile, ShowBlockProfileConfirmation);
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Instance.Unsubscribe<MatchDetailPageViewModel>(this, MessageNames.SendContactDetails);
            MessagingCenter.Instance.Unsubscribe<MatchDetailPageViewModel>(this, MessageNames.Unmatch);
            MessagingCenter.Instance.Unsubscribe<MatchDetailPageViewModel>(this, MessageNames.ReportProfile);
            MessagingCenter.Instance.Unsubscribe<MatchDetailPageViewModel>(this, MessageNames.BlockProfile);
            base.OnDisappearing();

        }

        private void ShowContactDetailsConfirmation(MatchDetailPageViewModel vm)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Reveal contact details",
                    "Do you want to reveal your contact details to your match?", "Send", "Cancel");

                vm.ShowContactDetailsCallBack(result);
            });
        }

        private void ShowUnmatchConfirmation(MatchDetailPageViewModel vm)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Unmatch",
                    "Are you sure you want to unmatch with this person?", "Unmatch", "Cancel");

                await vm.UnmatchCallback(result);
            });
        }

        private void ShowBlockProfileConfirmation(MatchDetailPageViewModel vm)
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

        private void ShowReportProfileDialog(MatchDetailPageViewModel vm)
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