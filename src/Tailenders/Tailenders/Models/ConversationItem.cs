using System;

namespace Tailenders.Models
{
    public class ConversationItem
    {
        public string ProfileId { get; set; }
        public string Message { get; set; }
        public bool IsOutgoing { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}