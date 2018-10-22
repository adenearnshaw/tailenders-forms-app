using System;
using System.Collections.Generic;
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
           typeof(float),
           typeof(FormSliderFieldView),
           1f,
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (FormSliderFieldView)bindable;
               ctrl.MinValue = (float)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);
        
        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(
           nameof(MaxValue),
           typeof(float),
           typeof(FormSliderFieldView),
           100f,
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (FormSliderFieldView)bindable;
               ctrl.MaxValue = (float)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty LowerValueProperty = BindableProperty.Create(
            nameof(LowerValue),
            typeof(float),
            typeof(FormSliderFieldView),
            30f,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormSliderFieldView)bindable;
                ctrl.LowerValue = (float)newValue;
            },
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty UpperValueProperty = BindableProperty.Create(
            nameof(UpperValue),
            typeof(float),
            typeof(FormSliderFieldView),
            60f,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormSliderFieldView)bindable;
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
