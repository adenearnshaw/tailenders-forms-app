namespace Tailenders.Navigation
{
    public interface INavigationAware
    {
        void OnNavigatedToAsync(object navigationParams);
        void OnNavigatingFrom();
    }
}
