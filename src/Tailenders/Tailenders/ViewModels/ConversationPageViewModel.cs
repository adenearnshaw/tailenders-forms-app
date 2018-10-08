using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
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
            Messages = new ObservableCollection<ConversationItemViewModel>();
            SubmitMessageCommand = new RelayCommand<string>(async(msg) => await SubmitMessage(msg));
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

        private string _pendingMessage;
        public string PendingMessage
        {
            get => _pendingMessage;
            set => Set(ref _pendingMessage, value);
        }

        public ObservableCollection<ConversationItemViewModel> Messages { get; set; }

        public ICommand SubmitMessageCommand { get; private set; }

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
                Messages.Add(new ConversationItemViewModel(conversationItem));
            }

            if (!Messages.Any()){
                Messages.Add(new ConversationItemViewModel(new ConversationItem
                {
                    ProfileId = _profileItem.Id,
                    IsOutgoing = false,
                    Message = "Hey, did I just knick a ball to you while you were standing directly behind me?\r\n\r\nBecause I think I have a hot spot for you, and I reckon your a keeper.",
                    TimeStamp = DateTime.UtcNow
                }));
            }
            RaisePropertyChanged(nameof(Messages));
        }

        public override void OnNavigatingFrom()
        {
            var allMessages = Messages.Select(m => new ConversationItem
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

        private async Task SubmitMessage(string message)
        {
            var newMsg = new ConversationItem
            {
                ProfileId = "user_1",
                IsOutgoing = true,
                Message = message,
                TimeStamp = DateTime.Now
            };

            Messages.Add(new ConversationItemViewModel(newMsg));

            await Task.Delay(2500);

            Messages.Add(new ConversationItemViewModel(new ConversationItem
            {
                ProfileId = _profileItem.Id,
                IsOutgoing = false,
                Message = "Haha yeah totally",
                TimeStamp = DateTime.UtcNow
            }));

            PendingMessage = string.Empty;
        }
    }
}