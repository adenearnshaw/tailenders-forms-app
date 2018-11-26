using Xamarin.Forms;

namespace Tailenders.Views
{
    public partial class FormRangeSliderFieldView : BaseFormFieldView
    {
        public FormRangeSliderFieldView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty MinValueProperty = BindableProperty.Create(
           nameof(MinValue),
           typeof(float),
           typeof(FormRangeSliderFieldView),
           1f,
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (FormRangeSliderFieldView)bindable;
               ctrl.MinValue = (float)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);
        
        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(
           nameof(MaxValue),
           typeof(float),
           typeof(FormRangeSliderFieldView),
           100f,
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (FormRangeSliderFieldView)bindable;
               ctrl.MaxValue = (float)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty LowerValueProperty = BindableProperty.Create(
            nameof(LowerValue),
            typeof(float),
            typeof(FormRangeSliderFieldView),
            30f,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormRangeSliderFieldView)bindable;
                ctrl.LowerValue = (float)newValue;
            },
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty UpperValueProperty = BindableProperty.Create(
            nameof(UpperValue),
            typeof(float),
            typeof(FormRangeSliderFieldView),
            60f,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormRangeSliderFieldView)bindable;
                ctrl.UpperValue = (float)newValue;
            },
            defaultBindingMode: BindingMode.TwoWay);

        public float MinValue
        {
            get => (float)base.GetValue(MinValueProperty);
            set => base.SetValue(MinValueProperty, value);
        }

        public float MaxValue
        {
            get => (float)base.GetValue(MaxValueProperty);
            set => base.SetValue(MaxValueProperty, value);
        }

        public float LowerValue
        {
            get => (float)base.GetValue(LowerValueProperty);
            set => base.SetValue(LowerValueProperty, value);
        }

        public float UpperValue
        {
            get => (float)base.GetValue(UpperValueProperty);
            set => base.SetValue(UpperValueProperty, value);
        }
    }
}
