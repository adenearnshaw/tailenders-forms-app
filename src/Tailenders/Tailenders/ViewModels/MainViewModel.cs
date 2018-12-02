using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MLToolkit.Forms.SwipeCardView.Core;
using Tailenders.Managers;
using Tailenders.Navigation;

namespace Tailenders.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPairingsManager _pairingsManager;

        public MainViewModel(INavigationService navigationService,
                             IPairingsManager pairingsManager)
        {
            _navigationService = navigationService;
            _pairingsManager = pairingsManager;

            CardItems = new ObservableCollection<CardItemViewModel>();
            Threshold = (uint)(App.ScreenWidth / 3);

            CardSwipedCommand = new RelayCommand<SwipedCardEventArgs>(CardSwiped);
            SearchAgainCommand = new RelayCommand(async () => await ReloadData());
            ProfileLikedCommand = new RelayCommand<CardItemViewModel>(OnProfileLiked);
            ProfileDiscardedCommand = new RelayCommand<CardItemViewModel>(OnProfileDisliked);
            NavigateToPartnershipsCommand = new RelayCommand(NavigateToPartnerships);

            ReloadData();
        }

        private uint _threshold;
        public uint Threshold
        {
            get => _threshold;
            set => Set(ref _threshold, value);
        }

        public bool HasProfilesToView => _topItem != null;
        public bool HasNoProfilesToView => _topItem == null;

        private CardItemViewModel _topItem;
        public CardItemViewModel TopItem
        {
            get => _topItem;
            set
            {
                Set(ref _topItem, value);
                RaisePropertyChanged(nameof(HasProfilesToView));
                RaisePropertyChanged(nameof(HasNoProfilesToView));
            }
        }

        public ObservableCollection<CardItemViewModel> CardItems { get; set; }

        public ICommand CardSwipedCommand { get; private set; }
        public ICommand ProfileDiscardedCommand { get; private set; }
        public ICommand ProfileLikedCommand { get; private set; }
        public ICommand SearchAgainCommand { get; private set; }
        public ICommand FinishedSwipingCommand { get; private set; }
        public ICommand NavigateToPartnershipsCommand { get; private set; }

        private async Task ReloadData()
        {
            var data = await _pairingsManager.SearchForPairings();

            CardItems.Clear();
            foreach (var card in data)
            {
                CardItems.Add(new CardItemViewModel(card));
            }
            RaisePropertyChanged(nameof(CardItems));
        }

        private void OnProfileLiked(CardItemViewModel item)
        {
            MatchesManager.Instance.AddProfileToMatches(item.Data);
            //AddNewProfileItem(itemIndex);
        }

        private void OnProfileDisliked(CardItemViewModel item)
        {
            //AddNewProfileItem(itemIndex);
        }

        private void AddNewProfileItem(int itemIndex)
        {
            //var elementIndex = itemIndex % 5;
            //var data = ProfileRetriever.Instance.GetProfilesAsCards().ElementAtOrDefault(elementIndex);

            //if (data != null)
            //{
            //    CardItems.Add(new CardItemViewModel(data));
            //}
        }

        private void NavigateToPartnerships()
        {
            _navigationService.NavigateTo(PageKeys.MatchesPage);
        }

        private void CardSwiped(SwipedCardEventArgs args)
        {
            if (args.Direction == SwipeCardDirection.Left)
            {
                OnProfileDisliked((CardItemViewModel)args.Item);
            }
            else if (args.Direction == SwipeCardDirection.Right)
            {
                OnProfileLiked((CardItemViewModel)args.Item);
            }
        }
    }
}
