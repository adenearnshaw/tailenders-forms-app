using Tailenders.Views;

namespace Tailenders.Navigation
{
    public static class NavigationServiceFactory
    {
        /// <summary>
        /// Builds the NavigationService and registers all pages with an appropriate key
        /// </summary>
        /// <returns></returns>
        public static INavigationService Get()
        {
            var navigationService = new FormsNavigationService();

            navigationService.Configure(PageKeys.HomePage, typeof(MainPage));
            navigationService.Configure(PageKeys.MatchesPage, typeof(MatchesPage));
            navigationService.Configure(PageKeys.ConversationPage, typeof(ConversationPage));

            return navigationService;
        }
    }
}