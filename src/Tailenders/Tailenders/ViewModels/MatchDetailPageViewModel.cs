using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tailenders.Common;
using Tailenders.Managers;
using Tailenders.Navigation;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class MatchDetailPageViewModel : BaseViewModel
    {
        private readonly IMatchesManager _matchesManager;
        private readonly INavigationService _navigationService;

        public MatchDetailPageViewModel(IMatchesManager matchesManager,
                                        INavigationService navigationService)
        {
            _matchesManager = matchesManager;
            _navigationService = navigationService;

            ShowContactDetailsCommand = new RelayCommand(ShowContactDetails);
            UnmatchCommand = new RelayCommand(Unmatch);
        }

        private MatchItemViewModel _matchItem;
        public MatchItemViewModel MatchItem
        {
            get => _matchItem;
            set => Set(ref _matchItem, value);
        }

        private bool _showRevealDetailsButton;
        public bool ShowRevealDetailsButton
        {
            get => _showRevealDetailsButton;
            set => Set(ref _showRevealDetailsButton, value);
        }

        public ICommand ShowContactDetailsCommand { get; }
        public ICommand UnmatchCommand { get; }

        public override void OnNavigatedTo(object navigationParams)
        {
            base.OnNavigatedTo(navigationParams);

            MatchItem = (navigationParams as MatchItemViewModel) ?? new MatchItemViewModel();
            ShowRevealDetailsButton = !MatchItem.MatchDetail.UserContactDetailsVisible;
        }
        

        private void ShowContactDetails()
        {
            MessagingCenter.Instance.Send(this, MessageNames.SendContactDetails);
        }

        public async Task ShowContactDetailsCallBack(bool result)
        {
            if (!result)
                return;

            ShowRevealDetailsButton = false;
            _matchesManager.UpdateMatchContractProfile(MatchItem.MatchDetail, true);
        }

        private void Unmatch()
        {
            MessagingCenter.Instance.Send(this, MessageNames.Unmatch);
        }

        public async Task UnmatchCallback(bool result)
        {
            if (!result)
                return;

            await _matchesManager.Unmatch(MatchItem.MatchDetail);
            _navigationService.GoBack();
        }
    }
}