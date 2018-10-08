using System;
using Tailenders.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Tailenders")]
[assembly: ExportEffect(typeof(TopPaddingWhenKeyboardAppearsEffect), "TopPaddingWhenKeyboardAppearsEffect")]
namespace Tailenders.iOS.Effects
{
    public class TopPaddingWhenKeyboardAppearsEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            UIKeyboard.Notifications.ObserveWillShow((sender, args) =>
            {
                if (Element != null)
                {
                    //((View)Element).Margin = new Thickness(0, 0, 0, args.FrameEnd.Height);
                    ((View)Element).TranslateTo(0, -args.FrameEnd.Height, 0);
                }
            });

            UIKeyboard.Notifications.ObserveWillHide((sender, args) =>
            {
                if (Element != null)
                {
                    //((View)Element).Margin = new Thickness(0);
                    ((View)Element).TranslateTo(0, 0, 0);
                }
            });

            //UIKeyboard.Notifications.ObserveDidChangeFrame((sender, args) =>
            //{
            //    ((View)Element).Margin = new Thickness(0, args.FrameEnd.Height, 0, 0);
            //});
        }

        protected override void OnDetached()
        {

        }
    }
}
