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
            SimpleIoc.Default.Register<IMatchesClient, MatchesClient>();
            SimpleIoc.Default.Register<IPairingsClient, PairingsClient>();
            SimpleIoc.Default.Register<IProfileImageUploader, ProfileImageUploader>();
            SimpleIoc.Default.Register<IProfilesClient, ProfilesClient>();

            //Managers
            SimpleIoc.Default.Register<IMatchesManager, MatchesManager>();
            SimpleIoc.Default.Register<IPairingsManager, PairingsManager>();
            SimpleIoc.Default.Register<IProfileManager, ProfileManager>();

            //ViewModels
            SimpleIoc.Default.Register<AboutPageViewModel>();
            SimpleIoc.Default.Register<BlockedProfilePageViewModel>();
            SimpleIoc.Default.Register<ConversationPageViewModel>();
            SimpleIoc.Default.Register<DeleteProfilePageViewModel>();
            SimpleIoc.Default.Register<LoginPageViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MatchDetailPageViewModel>();
            SimpleIoc.Default.Register<MatchesPageViewModel>();
            SimpleIoc.Default.Register<MenuPageViewModel>();
            SimpleIoc.Default.Register<NewProfilePageViewModel>();
            SimpleIoc.Default.Register<PodcastPageViewModel>();
            SimpleIoc.Default.Register<ProfilePageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
        }

        public AboutPageViewModel About => SimpleIoc.Default.GetInstance<AboutPageViewModel>();
        public BlockedProfilePageViewModel BlockedProfile => SimpleIoc.Default.GetInstance<BlockedProfilePageViewModel>();
        public ConversationPageViewModel Conversation => SimpleIoc.Default.GetInstance<ConversationPageViewModel>();
        public DeleteProfilePageViewModel DeleteProfile => SimpleIoc.Default.GetInstance<DeleteProfilePageViewModel>();
        public LoginPageViewModel Login => SimpleIoc.Default.GetInstance<LoginPageViewModel>();
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public MatchDetailPageViewModel MatchDetail => SimpleIoc.Default.GetInstance<MatchDetailPageViewModel>();
        public MatchesPageViewModel Matches => SimpleIoc.Default.GetInstance<MatchesPageViewModel>();
        public MenuPageViewModel Menu => SimpleIoc.Default.GetInstance<MenuPageViewModel>();
        public NewProfilePageViewModel NewProfile => SimpleIoc.Default.GetInstance<NewProfilePageViewModel>();
        public PodcastPageViewModel Podcast => SimpleIoc.Default.GetInstance<PodcastPageViewModel>();
        public ProfilePageViewModel Profile => SimpleIoc.Default.GetInstance<ProfilePageViewModel>();
        public SettingsPageViewModel Settings => SimpleIoc.Default.GetInstance<SettingsPageViewModel>();
    }
}
