using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tailenders.Data;

namespace Tailenders.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ProfileRetriever _profileRetriever;

        public MainViewModel()
        {
            _profileRetriever = new ProfileRetriever();

            CardItems = new ObservableCollection<CardItemViewModel>();
            CardItems.CollectionChanged += (sender, e) =>
            {
                RaisePropertyChanged(nameof(HasProfilesToView));
                RaisePropertyChanged(nameof(HasNoProfilesToView));
            };

            SearchAgainCommand = new RelayCommand(ReloadData);
            ProfileLikedCommand = new RelayCommand<CardItemViewModel>(RemoveItem);
            ProfileDiscardedCommand = new RelayCommand<CardItemViewModel>(RemoveItem);

            ReloadData();
        }

        public string Title => "Blower";

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

        private void ReloadData()
        {
            var data = _profileRetriever.GetProfilesAsCards();

            CardItems.Clear();
            foreach (var card in data)
            {
                CardItems.Add(card);
            }
            RaisePropertyChanged(nameof(CardItems));
        }

        private void RemoveItem(CardItemViewModel item)
        {
            //CardItems.Remove(item);
            Debug.WriteLine(item.Name);
        }
    }
}
