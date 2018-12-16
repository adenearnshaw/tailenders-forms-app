using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Tailenders.Common;
using Tailenders.Managers;
using TailendersApi.Contracts;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class ProfilePreviewPageViewModel : BaseViewModel
    {
        private readonly IProfileManager _profileManager;
        private readonly IPairingsManager _pairingsManager;

        public ProfilePreviewPageViewModel(CardItemViewModel cardItem)
        {
            _pairingsManager = SimpleIoc.Default.GetInstance<IPairingsManager>();
            _profileManager = SimpleIoc.Default.GetInstance<IProfileManager>();

            CardItem = cardItem;

            RequestBlockCommand = new RelayCommand(RequestBlockProfile);
            RequestReportCommand = new RelayCommand(RequestReportProfile);
        }

        private CardItemViewModel _cardItem;
        public CardItemViewModel CardItem
        {
            get => _cardItem;
            set => Set(ref _cardItem, value);
        }

        public ICommand RequestReportCommand { get; }
        public ICommand RequestBlockCommand { get; }

        public async Task ReportProfileCallback(ReportProfileReason result)
        {
            await _profileManager.ReportUser(CardItem.ProfileId, result);
        }

        public async Task BlockProfileCallback(bool result)
        {
            if (!result)
                return;

            await _pairingsManager.BlockPairing(CardItem.ProfileId);
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
