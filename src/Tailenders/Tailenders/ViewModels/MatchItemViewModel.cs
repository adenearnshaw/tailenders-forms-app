using System.Linq;
using Tailenders.Common;
using TailendersApi.Contracts;

namespace Tailenders.ViewModels
{
    public class MatchItemViewModel
    {
        public MatchItemViewModel()
        {
        }

        public MatchItemViewModel(MatchDetail detail)
        {
            Id = detail.Id;
            Name = detail.MatchedProfile.Name;
            ProfileUrl = detail.MatchedProfile
                               .Images
                               .OrderByDescending(i => i.UpdatedAt)
                               .FirstOrDefault()?
                               .ImageUrl ?? "te_avatar_default.jpg";
            MatchedAt = detail.MatchedAt.ToLocalTime().ToString("D");
            Bio = detail.MatchedProfile.Bio;
            Age = detail.MatchedProfile.Age.ToString();
            ShowAge = detail.MatchedProfile.ShowAge;
            FavouritePosition 
                = EnumHelper<CricketPosition>.GetDisplayValue((CricketPosition)detail.MatchedProfile.FavouritePosition);
            Location = detail.MatchedProfile.Location;
            ContactDetails = detail.MatchedProfile.ContactDetails;

            MatchDetail = detail;
        }

        public string Id { get; }
        public string Name { get; }
        public string ProfileUrl { get; }
        public string MatchedAt { get; }
        public string Bio { get; }
        public string Age { get; }
        public bool ShowAge { get; }
        public string FavouritePosition { get; }
        public string Location { get; }
        public string ContactDetails { get; }
        public bool HasContactDetails => !string.IsNullOrWhiteSpace(ContactDetails);

        public MatchDetail MatchDetail { get; }
    }
}