using System;
using Tailenders.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(CustomPageRenderer))]
namespace Tailenders.iOS.CustomRenderers
{
    public class CustomPageRenderer : PageRenderer
    {
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            var navctrl = this.ViewController.NavigationController;

            if (navctrl?.InteractivePopGestureRecognizer != null)
                navctrl.InteractivePopGestureRecognizer.Enabled = false;
        }
    }
}
