using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tailenders.Data;
using Tailenders.Navigation;
using Xamarin.Forms.Internals;

namespace Tailenders.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            CardItems = new ObservableCollection<CardItemViewModel>();
            CardItems.CollectionChanged += (sender, e) =>
            {
                RaisePropertyChanged(nameof(HasProfilesToView));
                RaisePropertyChanged(nameof(HasNoProfilesToView));
            };

            SearchAgainCommand = new RelayCommand(ReloadData);
            ProfileLikedCommand = new RelayCommand<int>(OnProfileLiked);
            ProfileDiscardedCommand = new RelayCommand<int>(OnProfileDisliked);
            NavigateToPartnershipsCommand = new RelayCommand(NavigateToPartnerships);

            ReloadData();
        }
        
        private bool _hasProfilesToView;
        public bool HasProfilesToView
        {
            get
            {
                _hasProfilesToView = CardItems.Any();
                return _hasProfilesToView;
            }
        }
        public bool HasNoProfilesToView => !_hasProfilesToView;

        public ObservableCollection<CardItemViewModel> CardItems { get; set; }

        public ICommand ProfileDiscardedCommand { get; private set; }
        public ICommand ProfileLikedCommand { get; private set; }
        public ICommand SearchAgainCommand { get; private set; }
        public ICommand FinishedSwipingCommand { get; private set; }
        public ICommand NavigateToPartnershipsCommand { get; private set; }

        private void ReloadData()
        {
            var data = ProfileRetriever.Instance.GetProfilesAsCards();

            CardItems.Clear();
            foreach (var card in data)
            {
                CardItems.Add(new CardItemViewModel(card));
            }
            RaisePropertyChanged(nameof(CardItems));
        }

        private void OnProfileLiked(int itemIndex)
        {
            var likedProfile = CardItems.ElementAt(itemIndex);
            MatchesManager.Instance.AddProfileToMatches(likedProfile.Data);

            AddNewProfileItem(itemIndex);
        }

        private void OnProfileDisliked(int itemIndex)
        {
            AddNewProfileItem(itemIndex);
        }

        private void AddNewProfileItem(int itemIndex)
        {
            var elementIndex = itemIndex % 5;
            var data = ProfileRetriever.Instance.GetProfilesAsCards().ElementAtOrDefault(elementIndex);

            if (data != null)
            {
                CardItems.Add(new CardItemViewModel(data));
            }
        }

        private void NavigateToPartnerships()
        {
            _navigationService.NavigateTo(PageKeys.MatchesPage);
        }
    }
}
