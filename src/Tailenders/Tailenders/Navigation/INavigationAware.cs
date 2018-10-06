namespace Tailenders.Navigation
{
    public interface INavigationAware
    {
        void OnNavigatedTo(object navigationParams);
        void OnNavigatedFrom();
    }
}
