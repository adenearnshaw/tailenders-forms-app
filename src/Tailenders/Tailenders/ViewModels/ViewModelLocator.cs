using GalaSoft.MvvmLight.Ioc;
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

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MatchesPageViewModel>();
            SimpleIoc.Default.Register<ConversationPageViewModel>();

        }

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public MatchesPageViewModel Matches => SimpleIoc.Default.GetInstance<MatchesPageViewModel>();
        public ConversationPageViewModel Conversation => SimpleIoc.Default.GetInstance<ConversationPageViewModel>();
    }
}
