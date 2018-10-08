using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tailenders.Data;

namespace Tailenders.ViewModels
{
    public class MatchesPageViewModel : BaseViewModel
    {
        public MatchesPageViewModel()
        {
            Matches = new ObservableCollection<MatchItemViewModel>();
            RefreshDataCommand = new RelayCommand(RefreshMatches);
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
    }
}