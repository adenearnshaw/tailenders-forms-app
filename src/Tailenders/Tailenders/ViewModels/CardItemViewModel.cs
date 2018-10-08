using System;
using GalaSoft.MvvmLight;
using Tailenders.Models;

namespace Tailenders.ViewModels
{
    public class CardItemViewModel : ViewModelBase
    {
        public CardItemViewModel(ProfileItem profileItem)
        {
            Data = profileItem;
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
