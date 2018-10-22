using System;
using System.ComponentModel;
using Android.Content;
using Android.Runtime;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Tailenders.Controls.RangeSlider;
using Tailenders.Droid.Controls;
using Tailenders.Droid.CustomRenderers;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(RangeSliderView), typeof(RangeSliderRenderer))]
namespace Tailenders.Droid.CustomRenderers
{
    [Preserve(AllMembers = true)]
    public class RangeSliderRenderer : ViewRenderer<RangeSliderView, RangeSliderControl>
    {
        private bool _gestureEnabledPreviousState;

        public RangeSliderRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<RangeSliderView> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;
            if (Control == null)
            {
                var rangeSeekBar = new RangeSliderControl(Context)
                {
                    NotifyWhileDragging = true,
                    TextAboveThumbsColor = Color.Black
                };
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
            control.SetRangeValues(element.MinimumValue, element.MaximumValue);
            control.SetSelectedMinValue(element.LowerValue);
            control.SetSelectedMaxValue(element.UpperValue);
            control.MinThumbHidden = element.MinThumbHidden;
            control.MinThumbTextHidden = element.MinThumbTextHidden;
            control.MaxThumbHidden = element.MaxThumbHidden;
            control.MaxThumbTextHidden = element.MaxThumbTextHidden;
            control.StepValue = element.StepValue;
            control.StepValueContinuously = element.StepValueContinuously;
            if (element.BarHeight.HasValue)
                control.SetBarHeight(element.BarHeight.Value);
            control.ShowTextAboveThumbs = element.ShowTextAboveThumbs;
            control.TextSizeInSp = (int)Font.SystemFontOfSize(element.TextSize).ToScaledPixel();
            control.TextFormat = element.TextFormat;
            if (element.TextColor != Xamarin.Forms.Color.Default)
                control.TextAboveThumbsColor = element.TextColor.ToAndroid();
            control.FormatLabel = element.FormatLabel;
            control.ActivateOnDefaultValues = true;
            if (element.ActiveColor != Xamarin.Forms.Color.Default)
                control.ActiveColor = element.ActiveColor.ToAndroid();
            control.MaterialUI = element.MaterialUI;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == RangeSliderView.LowerValueProperty.PropertyName)
            {
                Control.SetSelectedMinValue(Element.LowerValue);
            }
            else if (e.PropertyName == RangeSliderView.UpperValueProperty.PropertyName)
            {
                Control.SetSelectedMaxValue(Element.UpperValue);
            }
            else if (e.PropertyName == RangeSliderView.MinimumValueProperty.PropertyName || e.PropertyName == RangeSliderView.MaximumValueProperty.PropertyName)
            {
                Control.SetRangeValues(Element.MinimumValue, Element.MaximumValue);
            }
            else if (e.PropertyName == RangeSliderView.MaxThumbHiddenProperty.PropertyName)
            {
                Control.MaxThumbHidden = Element.MaxThumbHidden;
            }
            else if (e.PropertyName == RangeSliderView.MinThumbHiddenProperty.PropertyName)
            {
                Control.MinThumbHidden = Element.MinThumbHidden;
            }
            else if (e.PropertyName == RangeSliderView.StepValueProperty.PropertyName)
            {
                Control.StepValue = Element.StepValue;
            }
            else if (e.PropertyName == RangeSliderView.StepValueContinuouslyProperty.PropertyName)
            {
                Control.StepValueContinuously = Element.StepValueContinuously;
            }
            else if (e.PropertyName == RangeSliderView.BarHeightProperty.PropertyName)
            {
                if (Element.BarHeight.HasValue)
                    Control.SetBarHeight(Element.BarHeight.Value);
            }
            else if (e.PropertyName == RangeSliderView.ShowTextAboveThumbsProperty.PropertyName)
            {
                Control.ShowTextAboveThumbs = Element.ShowTextAboveThumbs;
                ForceFormsLayout();
            }
            else if (e.PropertyName == RangeSliderView.TextSizeProperty.PropertyName)
            {
                Control.TextSizeInSp = (int)Font.SystemFontOfSize(Element.TextSize).ToScaledPixel();
                ForceFormsLayout();
            }
            else if (e.PropertyName == RangeSliderView.TextFormatProperty.PropertyName)
            {
                Control.TextFormat = Element.TextFormat;
            }
            else if (e.PropertyName == RangeSliderView.TextColorProperty.PropertyName)
            {
                if (Element.TextColor != Xamarin.Forms.Color.Default)
                    Control.TextAboveThumbsColor = Element.TextColor.ToAndroid();
            }
            else if (e.PropertyName == RangeSliderView.FormatLabelProperty.PropertyName)
            {
                Control.FormatLabel = Element.FormatLabel;
            }
            else if (e.PropertyName == RangeSliderView.ActiveColorProperty.PropertyName)
            {
                if (Element.ActiveColor != Xamarin.Forms.Color.Default)
                    Control.ActiveColor = Element.ActiveColor.ToAndroid();
            }
            else if (e.PropertyName == RangeSliderView.MaterialUiProperty.PropertyName)
            {
                Control.MaterialUI = Element.MaterialUI;
            }
            else if (e.PropertyName == RangeSliderView.MinThumbTextHiddenProperty.PropertyName)
            {
                Control.MinThumbTextHidden = Element.MinThumbTextHidden;
            }
            else if (e.PropertyName == RangeSliderView.MaxThumbTextHiddenProperty.PropertyName)
            {
                Control.MaxThumbTextHidden = Element.MaxThumbTextHidden;
            }
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
            Element.OnUpperValueChanged(Control.GetSelectedMaxValue());
        }

        private void RangeSeekBarLowerValueChanged(object sender, EventArgs e)
        {
            Element.OnLowerValueChanged(Control.GetSelectedMinValue());
        }

        // TODO find less weird hack to make slider work on Master-Detail page
        private void DisableGestures()
        {
            var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            if (masterDetailPage != null)
            {
                _gestureEnabledPreviousState = masterDetailPage.IsGestureEnabled;
                masterDetailPage.IsGestureEnabled = false;
            }
        }

        private void RestoreGestures()
        {
            var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            if (masterDetailPage != null)
            {
                masterDetailPage.IsGestureEnabled = _gestureEnabledPreviousState;
            }
        }
    }
}