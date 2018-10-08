using System.Collections.Generic;
using Tailenders.Models;

namespace Tailenders.Data
{
    public class ProfileRetriever
    {
        private static ProfileRetriever _instance;
        public static ProfileRetriever Instance => _instance ?? (_instance = new ProfileRetriever());

        public IReadOnlyCollection<ProfileItem> GetProfilesAsCards()
        {
            var list = new List<ProfileItem>
            {
                new ProfileItem("Jimmy", "36", "Burnley", "te_avatar_jimmy.png"),
                new ProfileItem("Felix", "34", "Southwark", "te_avatar_felix.png"),
                new ProfileItem("Greg", "32", "Lewisham", "te_avatar_greg.png"),
                new ProfileItem("Mattchin", "", "Bristol", "te_avatar_mattchin.png"),
                new ProfileItem("Michael", "43", "Eccles", "te_avatar_michael.png")
            };
            return list;
        }
    }
}
