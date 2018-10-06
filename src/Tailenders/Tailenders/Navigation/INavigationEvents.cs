namespace Tailenders.Navigation
{
    public interface INavigationEvents
    {
        void OnNavigatedTo(object navigationParams);
        void OnNavigatedFrom();
    }
}
