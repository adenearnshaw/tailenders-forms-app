using Tailenders.Common;
using Tailenders.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tailenders.Views
{
    public partial class NewProfilePage : ContentPage
    {
        public NewProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<NewProfilePageViewModel>(this, MessageNames.NotOldEnough, (vm) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Not old enough", "You're not old enough to use this app in accordance with it's Terms & Conditions", "Ok");
                });
            });

            MessagingCenter.Subscribe<ProfilePageViewModel>(this, MessageNames.NoPickPhotoSupport, (vm) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Can't select photo", "Selecting a photo doesn't appear to be not supported", "Ok");
                });
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<NewProfilePageViewModel>(this, MessageNames.NotOldEnough);
            MessagingCenter.Unsubscribe<ProfilePageViewModel>(this, MessageNames.NoPickPhotoSupport);
        }
    }
}
