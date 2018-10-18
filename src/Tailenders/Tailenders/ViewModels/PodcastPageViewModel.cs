using System;
using Xamarin.Essentials;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class PodcastPageViewModel: BaseViewModel
    {
        private const string TailendersPodcastUri_Apple = "https://itunes.apple.com/gb/podcast/tailenders/id1017349610";
        private const string TailendersPodcastUri_Android = "https://www.bbc.co.uk/programmes/p02pcb4w";

        private readonly Uri _podcastUri;

        public PodcastPageViewModel()
        {
            _podcastUri = Device.RuntimePlatform == Device.iOS
                                ? new Uri(TailendersPodcastUri_Apple)
                                : new Uri(TailendersPodcastUri_Android);

            LaunchPodcastAppCommand = new RelayCommand(async () => await Browser.OpenAsync(_podcastUri, BrowserLaunchMode.External));
        }

        public ICommand LaunchPodcastAppCommand { get; }
    }
}
