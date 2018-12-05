using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tailenders.Managers;

namespace Tailenders.ViewModels
{
    public class MatchDetailPageViewModel : BaseViewModel
    {
        private readonly IMatchesManager _matchesManager;

        public MatchDetailPageViewModel(IMatchesManager matchesManager)
        {
            _matchesManager = matchesManager;

            ShowContactDetailsCommand = new RelayCommand(ShowContactDetails);
        }

        private MatchItemViewModel _matchItem;
        public MatchItemViewModel MatchItem
        {
            get => _matchItem;
            set => Set(ref _matchItem, value);
        }

        public ICommand ShowContactDetailsCommand { get; }

        public override void OnNavigatedTo(object navigationParams)
        {
            base.OnNavigatedTo(navigationParams);

            MatchItem = (navigationParams as MatchItemViewModel) ?? new MatchItemViewModel();
        }

        private void ShowContactDetails()
        {

        }
    }
}