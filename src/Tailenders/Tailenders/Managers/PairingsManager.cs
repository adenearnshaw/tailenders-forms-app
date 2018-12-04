using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailendersApi.Client;
using TailendersApi.Contracts;

namespace Tailenders.Managers
{
    public interface IPairingsManager
    {
        Task<List<SearchProfile>> SearchForPairings();
        Task<MatchResult> SendPairingDecision(string profileId, PairingDecision decision);
    }

    public class PairingsManager : IPairingsManager
    {
        private readonly IPairingsRetriever _pairingsRetriever;

        public PairingsManager(IPairingsRetriever pairingsRetriever)
        {
            _pairingsRetriever = pairingsRetriever;
        }

        public async Task<List<SearchProfile>> SearchForPairings()
        {
            var results = await _pairingsRetriever.SearchForProfiles();
            return results.ToList();
        }

        public async Task<MatchResult> SendPairingDecision(string profileId, PairingDecision decision)
        {
            var isMatch = await _pairingsRetriever.SendPairDecision(profileId, decision);
            return isMatch;
        }
    }
}

