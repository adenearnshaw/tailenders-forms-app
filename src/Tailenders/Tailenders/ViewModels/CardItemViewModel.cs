using System;
using System.Linq;
using GalaSoft.MvvmLight;
using Tailenders.Models;
using TailendersApi.Contracts;

namespace Tailenders.ViewModels
{
    public class CardItemViewModel : ViewModelBase
    {
        public CardItemViewModel(ProfileItem profileItem)
        {
            Data = profileItem;
        }

        public CardItemViewModel(SearchProfile profile)
        {
            Data = new ProfileItem(profile.Name, 
                                   profile.Age.ToString(), 
                                   profile.Location, 
                                   profile.Images
                                          .OrderByDescending(i => i.UpdatedAt)
                                          .FirstOrDefault()?.ImageUrl ?? "te_avatar_default.jpg");
        }

        public ProfileItem Data { get; }
        
        public string Name => Data.Name;
        public string Age => Data.Age;
        public string Location => Data.Location;
        public string PhotoUrl => Data.PhotoUrl;

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
