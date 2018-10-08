using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tailenders.Data;
using Tailenders.Navigation;

namespace Tailenders.ViewModels
{
    public class MatchesPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public MatchesPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Matches = new ObservableCollection<MatchItemViewModel>();
            RefreshDataCommand = new RelayCommand(RefreshMatches);
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

        public ObservableCollection<MatchItemViewModel> Matches { get; set; }

        public ICommand RefreshDataCommand { get; private set; }
        public ICommand NavigateToConversationCommand { get; private set; }

        public override void OnNavigatedToAsync(object navigationParams)
        {
            base.OnNavigatedToAsync(navigationParams);
            RefreshMatches();
        }

        private void RefreshMatches()
        {
            IsBusy = true;

            var userMatches = MatchesManager.Instance.GetMatches();

            Matches.Clear();

            foreach (var profileItem in userMatches)
            {
                var matchItemVm = new MatchItemViewModel(profileItem);
                Matches.Add(matchItemVm);
            }
            RaisePropertyChanged(nameof(Matches));
            HasMatches = Matches.Any();
            
            IsBusy = false;
        }

        private void NavigateToConversation(MatchItemViewModel match)
        {
            _navigationService.NavigateTo(PageKeys.ConversationPage, match.Data);
        }
    }
}