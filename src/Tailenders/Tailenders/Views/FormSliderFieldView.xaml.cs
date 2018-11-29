using Xamarin.Forms;

namespace Tailenders.Views
{
    public partial class FormSliderFieldView : BaseFormFieldView
    {
        public FormSliderFieldView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty MinValueProperty = BindableProperty.Create(
            nameof(MinValue),
            typeof(double),
            typeof(FormSliderFieldView),
            default(double),
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormSliderFieldView)bindable;
                ctrl.MinValue = (double)newValue;
            },
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(
            nameof(MaxValue),
            typeof(double),
            typeof(FormSliderFieldView),
            default(double),
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormSliderFieldView)bindable;
                ctrl.MaxValue = (double)newValue;
            },
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty SliderValueProperty = BindableProperty.Create(
            nameof(SliderValue),
            typeof(double),
            typeof(FormSliderFieldView),
            default(double),
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormSliderFieldView)bindable;
                ctrl.SliderValue = (double)newValue;
            },
            defaultBindingMode: BindingMode.TwoWay);

        public double MinValue
        {
            get => (double)base.GetValue(MinValueProperty);
            set => base.SetValue(MinValueProperty, value);
        }

        public double MaxValue
        {
            get => (double)base.GetValue(MaxValueProperty);
            set => base.SetValue(MaxValueProperty, value);
        }

        public double SliderValue
        {
            get => (double)base.GetValue(SliderValueProperty);
            set => base.SetValue(SliderValueProperty, value);
        }
    }
}
