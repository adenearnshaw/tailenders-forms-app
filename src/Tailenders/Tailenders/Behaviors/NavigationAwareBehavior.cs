using System;
using Tailenders.Navigation;
using Xamarin.Forms;

namespace Tailenders.Behaviors
{
    public class NavigationAwareBehavior : BehaviorBase<Page>
    {
        protected override void OnAttachedTo(Page bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Appearing += PageOnAppearing;
            bindable.Disappearing += PageOnDisappearing;
        }

        protected override void OnDetachingFrom(Page bindable)
        {
            bindable.Appearing -= PageOnAppearing;
            bindable.Disappearing -= PageOnDisappearing;
            base.OnDetachingFrom(bindable);
        }

        private void PageOnAppearing(object sender, EventArgs eventArgs)
        {
            var navigationAwareContext = BindingContext as INavigationAware;
            var page = sender as Page;

            if (navigationAwareContext == null || page == null)
                return;

            var navigationParam = page.GetNavigationArgs();
            navigationAwareContext.OnNavigatedTo(navigationParam);
        }

        private void PageOnDisappearing(object sender, EventArgs eventArgs)
        {
            var navigationAwareContext = BindingContext as INavigationAware;
            navigationAwareContext?.OnNavigatingFrom();
        }
    }
}
