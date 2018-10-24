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


        private float _minValue;
        public float MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                OnPropertyChanged();
            }
        }

        private float _maxValue;
        public float MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                OnPropertyChanged();
            }
        }

        private float _lowerValue;
        public float LowerValue
        {
            get { return _lowerValue; }
            set
            {
                _lowerValue = value;
                OnPropertyChanged();
            }
        }

        private float _upperValue;
        public float UpperValue
        {
            get { return _upperValue; }
            set
            {
                _upperValue = value;
                OnPropertyChanged();
            }
        }
    }
}
