using GalaSoft.MvvmLight.Ioc;
using Tailenders.Data;
using Tailenders.Managers;
using Tailenders.Navigation;
using TailendersApi.Client;

namespace Tailenders.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                var navSvc = NavigationServiceFactory.Get();
                SimpleIoc.Default.Register(() => navSvc);
            }

            //ApiClient
            var clientSettings = new ClientSettings("https://tailendersapi-uat.azurewebsites.net/");
            SimpleIoc.Default.Register<IClientSettings>(() => clientSettings);
            SimpleIoc.Default.Register<ICredentialsProvider, CredentialsProvider>();
            SimpleIoc.Default.Register<IPairingsRetriever, PairingsRetriever>();
            SimpleIoc.Default.Register<IProfileImageUploader, ProfileImageUploader>();
            SimpleIoc.Default.Register<IProfilesRetriever, ProfilesRetriever>();

            //Managers
            SimpleIoc.Default.Register<IPairingsManager, PairingsManager>();
            SimpleIoc.Default.Register<IProfileManager, ProfileManager>();

            //ViewModels
            SimpleIoc.Default.Register<ConversationPageViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MatchesPageViewModel>();
            SimpleIoc.Default.Register<MenuPageViewModel>();
            SimpleIoc.Default.Register<NewProfilePageViewModel>();
            SimpleIoc.Default.Register<PodcastPageViewModel>();
            SimpleIoc.Default.Register<ProfilePageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();

        }

        public ConversationPageViewModel Conversation => SimpleIoc.Default.GetInstance<ConversationPageViewModel>();
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public MatchesPageViewModel Matches => SimpleIoc.Default.GetInstance<MatchesPageViewModel>();
        public MenuPageViewModel Menu => SimpleIoc.Default.GetInstance<MenuPageViewModel>();
        public NewProfilePageViewModel NewProfile => SimpleIoc.Default.GetInstance<NewProfilePageViewModel>();
        public PodcastPageViewModel Podcast => SimpleIoc.Default.GetInstance<PodcastPageViewModel>();
        public ProfilePageViewModel Profile => SimpleIoc.Default.GetInstance<ProfilePageViewModel>();
        public SettingsPageViewModel Settings => SimpleIoc.Default.GetInstance<SettingsPageViewModel>();
    }
}
