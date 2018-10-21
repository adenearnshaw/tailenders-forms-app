using System.Threading.Tasks;

namespace Tailenders.Navigation
{
    public interface INavigationAware
    {
        void OnNavigatedTo(object navigationParams);
        void OnNavigatingFrom();
    }
}
