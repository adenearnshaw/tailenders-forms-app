using System.Linq;
using GalaSoft.MvvmLight;
using TailendersApi.Contracts;

namespace Tailenders.ViewModels
{
    public class CardItemViewModel : ViewModelBase
    {
        public CardItemViewModel(SearchProfile profile)
        {
            ProfileId = profile.Id;
            Name = profile.Name;
            Age = profile.Age.ToString();
            Location = profile.Location;
            PhotoUrl = profile.Images
                    .OrderByDescending(i => i.UpdatedAt)
                    .FirstOrDefault()?.ImageUrl ?? "te_avatar_default.jpg";
        }

        public string ProfileId { get; }
        public string Name { get; }
        public string Age { get; }
        public string Location { get; }
        public string PhotoUrl { get; }

        public string Description
        {
            get
            {
                var description = Name;
                if (!string.IsNullOrWhiteSpace(Age))
                {
                    description += $", {Age}";
                }
                return description;
            }
        }
    }
}
