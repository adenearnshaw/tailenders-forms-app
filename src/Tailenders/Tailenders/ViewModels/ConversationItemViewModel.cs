using System;
using GalaSoft.MvvmLight;

namespace Tailenders.ViewModels
{
    public class ConversationItemViewModel : ViewModelBase
    {
        public ConversationItemViewModel()
        {
            Message = "";
            IsOutgoing = false;
            TimeStamp = DateTime.UtcNow;
        }

        public string Message { get; set; }
        public bool IsOutgoing { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}