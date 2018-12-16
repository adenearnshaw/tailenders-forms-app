using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tailenders.Common;
using Tailenders.Managers;
using Tailenders.Navigation;
using TailendersApi.Contracts;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class MatchDetailPageViewModel : BaseViewModel
    {
        private readonly IMatchesManager _matchesManager;
        private readonly INavigationService _navigationService;
        private readonly IProfileManager _profileManager;

        public MatchDetailPageViewModel(IMatchesManager matchesManager,
                                        INavigationService navigationService,
                                        IProfileManager profileManager)
        {
            _matchesManager = matchesManager;
            _navigationService = navigationService;
            _profileManager = profileManager;

            ShowContactDetailsCommand = new RelayCommand(ShowContactDetails);
            UnmatchCommand = new RelayCommand(Unmatch);
            RequestBlockCommand = new RelayCommand(RequestBlockProfile);
            RequestReportCommand = new RelayCommand(RequestReportProfile);
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
        public ICommand RequestReportCommand { get; }
        public ICommand RequestBlockCommand { get; }

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

        public void ShowContactDetailsCallBack(bool result)
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

        public async Task ReportProfileCallback(ReportProfileReason result)
        {
            await _profileManager.ReportUser(MatchItem.MatchDetail.MatchedProfile.Id, result);
        }

        public async Task BlockProfileCallback(bool result)
        {
            if (!result)
                return;

            await _matchesManager.BlockMatch(MatchItem.MatchDetail);
        }

        private void RequestReportProfile()
        {
            MessagingCenter.Send(this, MessageNames.ReportProfile);
        }

        private void RequestBlockProfile()
        {
            MessagingCenter.Send(this, MessageNames.BlockProfile);
        }
    }
}