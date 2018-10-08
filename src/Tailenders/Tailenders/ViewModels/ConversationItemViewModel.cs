using System;
using GalaSoft.MvvmLight;
using Tailenders.Models;

namespace Tailenders.ViewModels
{
    public class ConversationItemViewModel : ViewModelBase
    {
        public ConversationItemViewModel(ConversationItem item)
        {
            Message = item.Message;
            IsOutgoing = item.IsOutgoing;
            TimeStamp = item.TimeStamp;
        }

        public string Message { get; set; }
        public bool IsOutgoing { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}