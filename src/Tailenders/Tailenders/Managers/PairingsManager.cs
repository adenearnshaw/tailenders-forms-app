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
        private readonly IPairingsClient _pairingsClient;

        public PairingsManager(IPairingsClient pairingsClient)
        {
            _pairingsClient = pairingsClient;
        }

        public async Task<List<SearchProfile>> SearchForPairings()
        {
            var results = await _pairingsClient.SearchForProfiles();
            return results?.ToList();
        }

        public async Task<MatchResult> SendPairingDecision(string profileId, PairingDecision decision)
        {
            var isMatch = await _pairingsClient.SendPairDecision(profileId, decision);
            return isMatch;
        }
    }
}

