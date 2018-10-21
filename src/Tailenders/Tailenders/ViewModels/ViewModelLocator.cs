using GalaSoft.MvvmLight.Ioc;
using Tailenders.Managers;
using Tailenders.Navigation;

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

            SimpleIoc.Default.Register<IProfileManager, ProfileManager>(false);

            SimpleIoc.Default.Register<ConversationPageViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MatchesPageViewModel>();
            SimpleIoc.Default.Register<MenuPageViewModel>();
            SimpleIoc.Default.Register<PodcastPageViewModel>();
            SimpleIoc.Default.Register<ProfilePageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();

        }

        public ConversationPageViewModel Conversation => SimpleIoc.Default.GetInstance<ConversationPageViewModel>();
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public MatchesPageViewModel Matches => SimpleIoc.Default.GetInstance<MatchesPageViewModel>();
        public MenuPageViewModel Menu => SimpleIoc.Default.GetInstance<MenuPageViewModel>();
        public PodcastPageViewModel Podcast => SimpleIoc.Default.GetInstance<PodcastPageViewModel>();
        public ProfilePageViewModel Profile => SimpleIoc.Default.GetInstance<ProfilePageViewModel>();
        public SettingsPageViewModel Settings => SimpleIoc.Default.GetInstance<SettingsPageViewModel>();
    }
}
