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
           typeof(int),
           typeof(FormSliderFieldView),
           1,
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (FormSliderFieldView)bindable;
               ctrl.MinValue = (int)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty CurrentValueProperty = BindableProperty.Create(
           nameof(CurrentValue),
           typeof(int),
           typeof(FormSliderFieldView),
           50,
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (FormSliderFieldView)bindable;
               ctrl.CurrentValue = (int)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(
           nameof(MaxValue),
           typeof(int),
           typeof(FormSliderFieldView),
           100,
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (FormSliderFieldView)bindable;
               ctrl.MaxValue = (int)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);

        private int _minValue;
        public int MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                OnPropertyChanged();
            }
        }

        private int _currentValue;
        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                OnPropertyChanged();
            }
        }

        private int _maxValue;
        public int MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                OnPropertyChanged();
            }
        }
    }
}
