using System;
using MLToolkit.Forms.SwipeCardView.Core;
using Tailenders.ViewModels;
using Xamarin.Forms;
// using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Tailenders.Views
{
    public partial class MainPage : ContentPage
    {
        public event EventHandler MenuClicked;

        public MainPage()
        {
            InitializeComponent();
            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SwipeCardView.Dragging += OnDragging;
        }

        private void MenuButtonClicked(object sender, EventArgs e)
        {
            MenuClicked?.Invoke(this, e);
        }

        public void SetIsBusyOverlay(bool isActive, string message)
        {
            this.IsBusyIndicator.IsActive = isActive;
            this.IsBusyIndicator.OverlayText = message;
        }

        private void OnDislikeClicked(object sender, EventArgs e)
        {
            this.SwipeCardView.InvokeSwipe(SwipeCardDirection.Left);
        }

        private void OnLikeClicked(object sender, EventArgs e)
        {
            this.SwipeCardView.InvokeSwipe(SwipeCardDirection.Right);
        }

        private void OnDragging(object sender, DraggingCardEventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;
            var nopeFrame = view.FindByName<Frame>("NopeFrame");
            var likeFrame = view.FindByName<Frame>("LikeFrame");
            var threshold = ((MainViewModel) this.BindingContext).Threshold;

            var draggedXPercent = e.DistanceDraggedX / threshold;
            var draggedYPercent = e.DistanceDraggedY / threshold;

            switch (e.Position)
            {
                case DraggingCardPosition.Start:
                    nopeFrame.Opacity = 0;
                    likeFrame.Opacity = 0;
                    nopeButton.Scale = 1;
                    likeButton.Scale = 1;
                    break;
                case DraggingCardPosition.UnderThreshold:
                    if (e.Direction == SwipeCardDirection.Left)
                    {
                        nopeFrame.Opacity = (-1) * draggedXPercent;
                        nopeButton.Scale = 1 + draggedXPercent / 2;
                    }
                    else if (e.Direction == SwipeCardDirection.Right)
                    {
                        likeFrame.Opacity = draggedXPercent;
                        likeButton.Scale = 1 - draggedXPercent / 2;
                    }
                    else if (e.Direction == SwipeCardDirection.Up)
                    {
                        //nopeFrame.Opacity = 0;
                        //likeFrame.Opacity = 0;
                        //nopeButton.Scale = 1;
                        //likeButton.Scale = 1;
                    }
                    break;
                case DraggingCardPosition.OverThreshold:
                    if (e.Direction == SwipeCardDirection.Left)
                    {
                        nopeFrame.Opacity = 1;
                    }
                    else if (e.Direction == SwipeCardDirection.Right)
                    {
                        likeFrame.Opacity = 1;
                    }
                    else if (e.Direction == SwipeCardDirection.Up)
                    {
                        nopeFrame.Opacity = 0;
                        likeFrame.Opacity = 0;
                    }
                    break;
                case DraggingCardPosition.FinishedUnderThreshold:
                    nopeFrame.Opacity = 0;
                    likeFrame.Opacity = 0;
                    nopeButton.Scale = 1;
                    likeButton.Scale = 1;
                    break;
                case DraggingCardPosition.FinishedOverThreshold:
                    nopeFrame.Opacity = 0;
                    likeFrame.Opacity = 0;
                    nopeButton.Scale = 1;
                    likeButton.Scale = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
