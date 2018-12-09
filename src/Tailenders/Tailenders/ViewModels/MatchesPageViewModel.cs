using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmHelpers;
using Tailenders.Managers;
using Tailenders.Navigation;

namespace Tailenders.ViewModels
{
    public class MatchesPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMatchesManager _matchesManager;

        public MatchesPageViewModel(INavigationService navigationService,
                                    IMatchesManager matchesManager)
        {
            _navigationService = navigationService;
            _matchesManager = matchesManager;

            Matches = new ObservableRangeCollection<MatchItemViewModel>();
            RefreshDataCommand = new RelayCommand(async() => await RefreshMatches());
            NavigateToConversationCommand = new RelayCommand<MatchItemViewModel>(NavigateToConversation);
        }

        private bool _hasMatches;
        public bool HasMatches
        {
            get => _hasMatches;
            set
            {
                Set(ref _hasMatches, value);
                RaisePropertyChanged(nameof(HasNoMatches));
            }
        }
        public bool HasNoMatches => !_hasMatches;

        public ObservableRangeCollection<MatchItemViewModel> Matches { get; set; }

        public ICommand RefreshDataCommand { get; private set; }
        public ICommand NavigateToConversationCommand { get; private set; }

        public override void OnNavigatedTo(object navigationParams)
        {
            base.OnNavigatedTo(navigationParams);
            RefreshMatches();
        }

        private async Task RefreshMatches()
        {
            IsBusy = true;

            var userMatches = await _matchesManager.GetMatches();

            //Matches.Clear();

            if (userMatches != null)
            {
                Matches.ReplaceRange(userMatches.Select(m => new MatchItemViewModel(m)));
                //RaisePropertyChanged(nameof(Matches));
            }

            HasMatches = Matches.Any();
            
            IsBusy = false;
        }

        private void NavigateToConversation(MatchItemViewModel match)
        {
            _navigationService.NavigateTo(PageKeys.MatchDetailPage, match);
            //_navigationService.NavigateTo(PageKeys.ConversationPage, null);
        }
    }
}