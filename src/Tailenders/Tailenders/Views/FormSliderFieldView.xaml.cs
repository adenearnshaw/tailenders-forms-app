using System;
using System.Diagnostics;
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

        private double _minValue;
        public double MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                OnPropertyChanged();
            }
        }

        private double _maxValue;
        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                OnPropertyChanged();
            }
        }

        private double _sliderValue;
        public double SliderValue
        {
            get { return _sliderValue; }
            set
            {
                _sliderValue = value;
                OnPropertyChanged();
            }
        }       
    }
}
