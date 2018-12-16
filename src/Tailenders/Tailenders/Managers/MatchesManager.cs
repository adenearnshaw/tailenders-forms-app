using System.Collections.Generic;
using System.Threading.Tasks;
using TailendersApi.Client;
using TailendersApi.Contracts;

namespace Tailenders.Managers
{
    public interface IMatchesManager
    {
        Task<IReadOnlyCollection<MatchDetail>> GetMatches();
        Task UpdateMatchContractProfile(MatchDetail match, bool show);
        Task Unmatch(MatchDetail match);
        Task BlockMatch(MatchDetail match);
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

        public async Task UpdateMatchContractProfile(MatchDetail match, bool show)
        {
            match.UserContactDetailsVisible = show;
            await _matchesClient.UpdateMatch(match);
        }

        public async Task Unmatch(MatchDetail match)
        {
            await _matchesClient.Unmatch(match);
        }

        public  async Task BlockMatch(MatchDetail match)
        {
            await _matchesClient.BlockMatch(match);
        }
    }
}
