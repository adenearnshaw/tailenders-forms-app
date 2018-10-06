using GalaSoft.MvvmLight;
using Tailenders.Navigation;

namespace Tailenders.ViewModels
{
    public class BaseViewModel : ViewModelBase, INavigationAware
    {
        public virtual void OnNavigatedTo(object navigationParams)
        {
        }

        public virtual void OnNavigatedFrom()
        {
        }
    }
}
