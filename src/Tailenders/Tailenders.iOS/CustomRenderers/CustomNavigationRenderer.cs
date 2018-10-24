using Plugin.Iconize;
using Tailenders.Controls.TransparentNavBar;
using Tailenders.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IconNavigationPage), typeof(CustomNavigationRenderer))]
namespace Tailenders.iOS.CustomRenderers
{
    public class CustomNavigationRenderer : IconNavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            //UINavigationBar.Appearance.ShadowImage = new UIImage();
            //UINavigationBar.Appearance.BackgroundColor = UIColor.Clear;
            //UINavigationBar.Appearance.TintColor = UIColor.White;
            //UINavigationBar.Appearance.BarTintColor = UIColor.Clear;
            //UINavigationBar.Appearance.Translucent = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}
