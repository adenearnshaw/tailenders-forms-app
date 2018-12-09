using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MLToolkit.Forms.SwipeCardView.Core;
using MvvmHelpers;
using Tailenders.Common;
using Tailenders.Managers;
using Tailenders.Navigation;
using TailendersApi.Contracts;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPairingsManager _pairingsManager;

        private int _numberOfCardsSwiped;

        public MainViewModel(INavigationService navigationService,
                             IPairingsManager pairingsManager)
        {
            _navigationService = navigationService;
            _pairingsManager = pairingsManager;

            CardItems = new ObservableRangeCollection<CardItemViewModel>();
            Threshold = (uint)(App.ScreenWidth / 3);

            CardSwipedCommand = new RelayCommand<SwipedCardEventArgs>(async (args) => await CardSwiped(args));
            SearchAgainCommand = new RelayCommand(async () => await RetrievePairings());
            NavigateToPartnershipsCommand = new RelayCommand(NavigateToPartnerships);

            LoadData();
        }

        private uint _threshold;
        public uint Threshold
        {
            get => _threshold;
            set => Set(ref _threshold, value);
        }

        private bool _hasProfilesToView;
        public bool HasProfilesToView

        {
            get => _hasProfilesToView;
            set => Set(ref _hasProfilesToView, value);
        }

        private bool _hasNoProfilesToView;
        public bool HasNoProfilesToView
        {
            get => _hasNoProfilesToView;
            set => Set(ref _hasNoProfilesToView, value);
        }

        public ObservableRangeCollection<CardItemViewModel> CardItems { get; set; }

        public ICommand CardSwipedCommand { get; private set; }
        public ICommand SearchAgainCommand { get; private set; }
        public ICommand NavigateToPartnershipsCommand { get; private set; }

        private async Task LoadData()
        {
            var data = await _pairingsManager.SearchForPairings();

            CardItems.Clear();
            _numberOfCardsSwiped = CardItems.Count;

            if (data != null)
            {
                CardItems.AddRange(data.Select(sp => new CardItemViewModel(sp)));
                RaisePropertyChanged(nameof(CardItems));
            }

            HasMorePairingsAvailable();
        }

        private void NavigateToPartnerships()
        {
            try
            {
                _navigationService.NavigateTo(PageKeys.MatchesPage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task CardSwiped(SwipedCardEventArgs args)
        {
            var swipedItem = args.Item as CardItemViewModel;
            if (swipedItem == null)
                return;

            _numberOfCardsSwiped++;
            var matchDecision = args.Direction == SwipeCardDirection.Right
                ? PairingDecision.Liked
                : PairingDecision.NotLiked;

            await CheckPairing(swipedItem.ProfileId, matchDecision);
            await RetrievePairings();


        }

        private async Task CheckPairing(string pairedProfileId, PairingDecision decision)
        {
            var result = await _pairingsManager.SendPairingDecision(pairedProfileId, decision);

            if (result != null && result.IsMatch)
            {
                MessagingCenter.Instance.Send(this, MessageNames.ProfileMatch);
            }
        }

        private async Task RetrievePairings()
        {
            IsBusy = true;
            if (CardItems.Count - _numberOfCardsSwiped <= 0)
            {
                var dataTask = _pairingsManager.SearchForPairings();
                await Task.WhenAll(new List<Task>
                {
                    dataTask,
                    Task.Delay(500)
                });
                if (dataTask?.Result != null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        CardItems.AddRange(dataTask.Result.Select(sp => new CardItemViewModel(sp)));
                        HasMorePairingsAvailable();
                    });
                }
            }

            HasMorePairingsAvailable();
            IsBusy = false;
        }

        private void HasMorePairingsAvailable()
        {
            var hasMorePairings = CardItems.Count > _numberOfCardsSwiped;

            HasProfilesToView = hasMorePairings;
            HasNoProfilesToView = !hasMorePairings;
        }
    }
}
