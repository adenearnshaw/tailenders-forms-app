using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tailenders.Models;

namespace Tailenders.Data
{
    public class MatchesManager
    {
        private static MatchesManager _instance;
        public static MatchesManager Instance => _instance ?? (_instance = new MatchesManager());

        public MatchesManager()
        {
            _matches = new List<ProfileItem>();
        }

        private List<ProfileItem> _matches;
        public IReadOnlyCollection<ProfileItem> GetMatches()
        {
            return _matches;
        }

        public void AddProfileToMatches(ProfileItem profile)
        {
            profile.MatchedAt = DateTime.UtcNow;
            _matches.Add(profile);
        }

        public void UpdateProfileItem(ProfileItem profile)
        {
            _matches.Remove(_matches.First(p => p.Id == profile.Id));
            _matches.Add(profile);
        }

        public Task LoadSavedData()
        {
            return Task.FromResult<object>(null);
        }

        public Task StoreData()
        {
            return Task.FromResult<object>(null);
        }
    }
}
