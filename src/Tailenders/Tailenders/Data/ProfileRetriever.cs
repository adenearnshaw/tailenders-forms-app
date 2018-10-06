using System.Collections.Generic;
using Tailenders.ViewModels;

namespace Tailenders.Data
{
    public class ProfileRetriever
    {
        public IReadOnlyCollection<CardItemViewModel> GetProfilesAsCards()
        {
            var list = new List<CardItemViewModel>
            {
                new CardItemViewModel("Jimmy", "36", "Burnley", "te_avatar_jimmy.png"),
                new CardItemViewModel("Felix", "34", "Southwark", "te_avatar_felix.png"),
                new CardItemViewModel("Greg", "32", "Lewisham", "te_avatar_greg.png"),
                new CardItemViewModel("Mattchin", "", "Bristol", "te_avatar_mattchin.png"),
                new CardItemViewModel("Michael", "43", "Eccles", "te_avatar_michael.png")
            };
            return list;
        }
    }
}
