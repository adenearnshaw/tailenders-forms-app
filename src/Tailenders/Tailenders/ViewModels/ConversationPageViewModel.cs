using System.Collections.ObjectModel;
using System.Linq;
using Tailenders.Data;
using Tailenders.Models;
using Tailenders.Navigation;

namespace Tailenders.ViewModels
{
    public class ConversationPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        private ProfileItem _profileItem;

        public ConversationPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _matchedOn;
        public string MatchedOn
        {
            get => _matchedOn;
            set => Set(ref _matchedOn, value);
        }

        private string _photoUrl;
        public string PhotoUrl
        {
            get => _photoUrl;
            set => Set(ref _photoUrl, value);
        }

        public ObservableCollection<ConversationItemViewModel> Messges { get; set; }

        public override void OnNavigatedToAsync(object navigationParams)
        {
            base.OnNavigatedToAsync(navigationParams);

            _profileItem = navigationParams as ProfileItem;

            if (_profileItem == null)
            {
                _navigationService.GoBack();
                return;
            }

            Name = _profileItem.Name;
            MatchedOn = _profileItem.MatchedAt.HasValue
                            ? $"Matched on: {_profileItem.MatchedAt.Value.ToShortDateString()}"
                            : "You're a match!";
            PhotoUrl = _profileItem.PhotoUrl;

            foreach (var conversationItem in _profileItem.Conversation.OrderBy(c => c.TimeStamp))
            {
                Messges.Add(new ConversationItemViewModel(conversationItem));
            }
            RaisePropertyChanged(nameof(Messges));
        }

        public override void OnNavigatingFrom()
        {
            var allMessages = Messges.Select(m => new ConversationItem
            {
                ProfileId = _profileItem.Id,
                IsOutgoing = m.IsOutgoing,
                Message = m.Message,
                TimeStamp = m.TimeStamp
            });

            _profileItem.Conversation = allMessages.ToList();
            MatchesManager.Instance.UpdateProfileItem(_profileItem);

            base.OnNavigatingFrom();
        }
    }
}