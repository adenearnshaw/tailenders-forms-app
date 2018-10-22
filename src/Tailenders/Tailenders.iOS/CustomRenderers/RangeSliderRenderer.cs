using System;
using System.ComponentModel;
using Foundation;
using Tailenders.Controls.RangeSlider;
using Tailenders.iOS.Controls;
using Tailenders.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RangeSliderView), typeof(RangeSliderRenderer))]
namespace Tailenders.iOS.CustomRenderers
{
    [Preserve(AllMembers = true)]
    public class RangeSliderRenderer : ViewRenderer<RangeSliderView, RangeSliderControl>
    {
        private bool _gestureEnabledPreviousState;
        protected override void OnElementChanged(ElementChangedEventArgs<RangeSliderView> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;
            if (Control == null)
            {
                var rangeSeekBar = new RangeSliderControl(Bounds);
                rangeSeekBar.LowerValueChanged += RangeSeekBarLowerValueChanged;
                rangeSeekBar.UpperValueChanged += RangeSeekBarUpperValueChanged;
                rangeSeekBar.DragStarted += RangeSeekBarDragStarted;
                rangeSeekBar.DragCompleted += RangeSeekBarDragCompleted;
                SetNativeControl(rangeSeekBar);
            }
            UpdateControl(Control, Element);
        }

        private void RangeSeekBarDragCompleted(object sender, EventArgs e)
        {
            RestoreGestures();
            Element.OnDragCompleted();
        }

        private void RangeSeekBarDragStarted(object sender, EventArgs e)
        {
            Element.OnDragStarted();
            DisableGestures();
        }

        private void UpdateControl(RangeSliderControl control, RangeSliderView element)
        {
            control.LowerValue = element.LowerValue;
            control.UpperValue = element.UpperValue;
            control.MinimumValue = element.MinimumValue;
            control.MaximumValue = element.MaximumValue;
            control.LowerHandleHidden = element.MinThumbHidden;
            control.LowerHandleLabelHidden = element.MinThumbTextHidden;
            control.UpperHandleHidden = element.MaxThumbHidden;
            control.UpperHandleLabelHidden = element.MaxThumbTextHidden;
            control.StepValue = element.StepValue;
            control.StepValueContinuously = element.StepValueContinuously;
            control.ShowTextAboveThumbs = element.ShowTextAboveThumbs;
            control.TextSize = (float)element.TextSize;
            control.TextFormat = element.TextFormat;
            if (element.TextColor != Color.Default)
                control.TextColor = element.TextColor.ToUIColor();
            control.FormatLabel = element.FormatLabel;
            control.TintColor = Color.FromHex("#333333").ToUIColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == RangeSliderView.LowerValueProperty.PropertyName)
            {
                Control.LowerValue = Element.LowerValue;
            }
            else if (e.PropertyName == RangeSliderView.UpperValueProperty.PropertyName)
            {
                Control.UpperValue = Element.UpperValue;
            }
            else if (e.PropertyName == RangeSliderView.MinimumValueProperty.PropertyName)
            {
                Control.MinimumValue = Element.MinimumValue;
            }
            else if (e.PropertyName == RangeSliderView.MaximumValueProperty.PropertyName)
            {
                Control.MaximumValue = Element.MaximumValue;
            }
            else if (e.PropertyName == RangeSliderView.MaxThumbHiddenProperty.PropertyName)
            {
                Control.UpperHandleHidden = Element.MaxThumbHidden;
            }
            else if (e.PropertyName == RangeSliderView.MinThumbHiddenProperty.PropertyName)
            {
                Control.LowerHandleHidden = Element.MinThumbHidden;
            }
            else if (e.PropertyName == RangeSliderView.MinThumbTextHiddenProperty.PropertyName)
            {
                Control.LowerHandleLabelHidden = Element.MinThumbTextHidden;
            }
            else if (e.PropertyName == RangeSliderView.MaxThumbTextHiddenProperty.PropertyName)
            {
                Control.UpperHandleLabelHidden = Element.MaxThumbTextHidden;
            }
            else if (e.PropertyName == RangeSliderView.StepValueProperty.PropertyName)
            {
                Control.StepValue = Element.StepValue;
            }
            else if (e.PropertyName == RangeSliderView.StepValueContinuouslyProperty.PropertyName)
            {
                Control.StepValueContinuously = Element.StepValueContinuously;
            }
            else if (e.PropertyName == RangeSliderView.ShowTextAboveThumbsProperty.PropertyName)
            {
                Control.ShowTextAboveThumbs = Element.ShowTextAboveThumbs;
                ForceFormsLayout();
            }
            else if (e.PropertyName == RangeSliderView.TextSizeProperty.PropertyName)
            {
                Control.TextSize = (float)Element.TextSize;
                ForceFormsLayout();
            }
            else if (e.PropertyName == RangeSliderView.TextFormatProperty.PropertyName)
            {
                Control.TextFormat = Element.TextFormat;
            }
            else if (e.PropertyName == RangeSliderView.TextColorProperty.PropertyName)
            {
                if (Element.TextColor != Color.Default)
                    Control.TextColor = Element.TextColor.ToUIColor();
            }
            else if (e.PropertyName == RangeSliderView.FormatLabelProperty.PropertyName)
            {
                Control.FormatLabel = Element.FormatLabel;
            }
            Control.SetNeedsLayout();
        }

        private void ForceFormsLayout()
        {
            //HACK to force Xamarin.Forms layout engine to update control size
            if (!Element.IsVisible) return;
            Element.IsVisible = false;
            Element.IsVisible = true;
        }

        private void RangeSeekBarUpperValueChanged(object sender, EventArgs e)
        {
            Element.OnUpperValueChanged(Control.UpperValue);
        }

        private void RangeSeekBarLowerValueChanged(object sender, EventArgs e)
        {
            Element.OnLowerValueChanged(Control.LowerValue);
        }

        // TODO find less weird hack to make slider work on Master-Detail page
        private void DisableGestures()
        {
            var masterDetailPage = Xamarin.Forms.Application.Current.MainPage as MasterDetailPage;
            if (masterDetailPage != null)
            {
                _gestureEnabledPreviousState = masterDetailPage.IsGestureEnabled;
                masterDetailPage.IsGestureEnabled = false;
            }
        }

        private void RestoreGestures()
        {
            var masterDetailPage = Xamarin.Forms.Application.Current.MainPage as MasterDetailPage;
            if (masterDetailPage != null)
            {
                masterDetailPage.IsGestureEnabled = _gestureEnabledPreviousState;
            }
        }
    }
}