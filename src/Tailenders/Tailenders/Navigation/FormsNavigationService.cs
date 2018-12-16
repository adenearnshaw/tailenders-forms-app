using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tailenders.Navigation
{
    public class FormsNavigationService : INavigationService
    {
        private Dictionary<string, Type> pages { get; }
            = new Dictionary<string, Type>();

        public Page MainPage => Application.Current.MainPage;

        public void Configure(string key, Type pageType) => pages[key] = pageType;

        public void GoBack() => MainPage.Navigation.PopAsync();

        public bool CanGoBack => MainPage.Navigation.NavigationStack.Count > 1;

        public void NavigateTo(string pageKey,
            object parameter = null,
            NavigationHistoryBehavior historyBehavior = NavigationHistoryBehavior.Default)
        {
            Type pageType;
            if (pages.TryGetValue(pageKey, out pageType))
            {
                var displayPage = (Page)Activator.CreateInstance(pageType);
                displayPage.SetNavigationArgs(parameter);

                if (historyBehavior == NavigationHistoryBehavior.ClearHistory)
                {
                    MainPage.Navigation.PopToRootAsync(true);

                    //  only push if its not the home page
                    if (MainPage.Navigation.NavigationStack[0].GetType() != pageType)
                        MainPage.Navigation.PushAsync(displayPage);
                }
                else
                {
                    MainPage.Navigation.PushAsync(displayPage);
                }
            }
            else
            {
                throw new ArgumentException($"No such page: {pageKey}.", nameof(pageKey));
            }
        }

        public async Task ShowModal(Page page)
        {
            await MainPage.Navigation.PushModalAsync(page);
        }

        public async Task HideModal()
        {
            while (MainPage.Navigation.ModalStack.Count > 0)
            {
                await MainPage.Navigation.PopModalAsync();
            }
        }

    }
}
