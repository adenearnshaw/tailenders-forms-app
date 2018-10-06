using GalaSoft.MvvmLight;

namespace Tailenders.ViewModels
{
    public class CardItemViewModel : ViewModelBase
    {
        public CardItemViewModel(string name, string age, string location, string photoUrl)
        {
            Name = name;
            Age = age;
            Location = location;
            PhotoUrl = photoUrl;
        }

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
