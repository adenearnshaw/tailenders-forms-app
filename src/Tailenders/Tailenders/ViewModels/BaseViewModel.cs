using GalaSoft.MvvmLight;
using Tailenders.Navigation;

namespace Tailenders.ViewModels
{
    public class BaseViewModel : ViewModelBase, INavigationEvents
    {
        public virtual void OnNavigatedTo(object navigationParams)
        {
        }

        public virtual void OnNavigatedFrom()
        {
        }
    }
}
