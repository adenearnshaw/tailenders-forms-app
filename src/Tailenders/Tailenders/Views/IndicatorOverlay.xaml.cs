using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailenders.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndicatorOverlay : ContentView
    {
        public static readonly BindableProperty IsActiveProperty
            = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(IndicatorOverlay), false,
                propertyChanged: IsActivePropertyChanged);

        public static readonly BindableProperty OverlayTextProperty
            = BindableProperty.Create(nameof(OverlayText), typeof(string), typeof(IndicatorOverlay), string.Empty);

        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public string OverlayText
        {
            get { return (string) GetValue(OverlayTextProperty); }
            set { SetValue(OverlayTextProperty, value); }
        }

        public IndicatorOverlay()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private static void IsActivePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IndicatorOverlay) bindable).IsVisible = (bool) newValue;
        }
    }
}