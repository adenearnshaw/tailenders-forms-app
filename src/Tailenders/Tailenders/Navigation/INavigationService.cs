using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tailenders.Navigation
{
    public enum NavigationHistoryBehavior
    {
        Default,
        ClearHistory
    }

    public interface INavigationService
    {
        void NavigateTo(string pageKey,
                        object parameter = null,
                        NavigationHistoryBehavior historyBehavior = NavigationHistoryBehavior.Default);

        bool CanGoBack { get; }
        void GoBack();
        Task ShowModal(Page page);
        Task HideModal();
    }

}