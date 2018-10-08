using System;
using System.Collections.Generic;

namespace Tailenders.Models
{
    public class ProfileItem
    {
        public ProfileItem(string name, string age, string location, string photoUrl)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Age = age;
            Location = location;
            PhotoUrl = photoUrl;

            Conversation = new List<ConversationItem>();
        }

        public string Id { get; }
        public string Name { get; }
        public string Age { get; }
        public string Location { get; }
        public string PhotoUrl { get; }
        
        public DateTime? MatchedAt { get; set; }
        public List<ConversationItem> Conversation { get; set; }
    }
}