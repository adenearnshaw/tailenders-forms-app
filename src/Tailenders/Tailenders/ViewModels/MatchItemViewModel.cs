using System;
using Tailenders.Models;

namespace Tailenders.ViewModels
{
    public class MatchItemViewModel
    {
        public MatchItemViewModel(ProfileItem data)
        {
            Data = data;
        }

        public ProfileItem Data { get; }

        public string Name => Data.Name;
        public string ProfileUrl => Data.PhotoUrl;
        public string MatchedAt => Data.MatchedAt.HasValue
                                    ? Data.MatchedAt.Value.ToLocalTime().ToString("D")
                                    : "";
    }
}