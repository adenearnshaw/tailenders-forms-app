using System;
using Xamarin.Forms;

namespace Tailenders.Behaviors
{
    public class PageDisappearingClearSelectedItemBehavior : BehaviorBase<Page>
    {
        public ListView ListViewToClear { get; set; }

        protected override void OnAttachedTo(Page bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Disappearing += PageOnDisappearing;
        }

        protected override void OnDetachingFrom(Page bindable)
        {
            bindable.Disappearing -= PageOnDisappearing;
            base.OnDetachingFrom(bindable);
        }

        private void PageOnDisappearing(object sender, EventArgs eventArgs)
        {
            if (ListViewToClear != null)
                ListViewToClear.SelectedItem = null;
        }
    }
}
