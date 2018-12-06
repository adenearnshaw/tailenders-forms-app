using System.Collections.Generic;
using System.Threading.Tasks;
using TailendersApi.Client;
using TailendersApi.Contracts;

namespace Tailenders.Managers
{
    public interface IMatchesManager
    {
        Task<IReadOnlyCollection<MatchDetail>> GetMatches();
    }

    public class MatchesManager : IMatchesManager
    {
        private readonly IMatchesClient _matchesClient;

        public MatchesManager(IMatchesClient matchesClient)
        {
            _matchesClient = matchesClient;
            _matches = new List<MatchDetail>();
        }

        private List<MatchDetail> _matches;

        public async Task<IReadOnlyCollection<MatchDetail>> GetMatches()
        {
            _matches = await _matchesClient.GetMatches();
            return _matches;
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
