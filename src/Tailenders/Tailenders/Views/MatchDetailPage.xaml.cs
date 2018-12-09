using Tailenders.Common;
using Tailenders.ViewModels;
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
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Instance.Unsubscribe<MatchDetailPageViewModel>(this, MessageNames.SendContactDetails);
            MessagingCenter.Instance.Unsubscribe<MatchDetailPageViewModel>(this, MessageNames.Unmatch);
            base.OnDisappearing();

        }

        private void ShowContactDetailsConfirmation(MatchDetailPageViewModel vm)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Reveal contact details",
                    "Do you want to reveal your contact details to your match?", "Send", "Cancel");

                await vm.ShowContactDetailsCallBack(result);
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
    }
}