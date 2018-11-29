using Tailenders.Common;
using Tailenders.Navigation;
using Tailenders.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailenders.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

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
            MessagingCenter.Unsubscribe<ProfilePageViewModel>(this, MessageNames.NoPickPhotoSupport);
        }
    }
}