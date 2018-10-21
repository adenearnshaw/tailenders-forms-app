using Xamarin.Forms;

namespace Tailenders.Views
{
    public class BaseFormFieldView : ContentView
    {
        public BaseFormFieldView()
        {
            HorizontalOptions = LayoutOptions.Fill;
        }

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            nameof(Value),
            typeof(string),
            typeof(BaseFormFieldView),
            string.Empty,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (BaseFormFieldView)bindable;
                ctrl.Value = (string)newValue;
            },
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
            nameof(Label),
            typeof(string),
            typeof(BaseFormFieldView),
            string.Empty,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (BaseFormFieldView)bindable;
                ctrl.Label = (string)newValue;
            },
            defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty ValidationMessageProperty = BindableProperty.Create(
           nameof(ValidationMessage),
           typeof(string),
           typeof(BaseFormFieldView),
           string.Empty,
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (BaseFormFieldView)bindable;
               ctrl.ValidationMessage = (string)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
           nameof(IsValid),
           typeof(bool?),
           typeof(BaseFormFieldView),
           null,
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (BaseFormFieldView)bindable;
               ctrl.IsValid = (bool?)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty IsReadonlyProperty = BindableProperty.Create(
           nameof(IsReadonly),
           typeof(bool),
           typeof(BaseFormFieldView),
           false,
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (BaseFormFieldView)bindable;
               ctrl.IsReadonly = (bool)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                OnPropertyChanged();
            }
        }
        private string _validationMessage;
        public string ValidationMessage
        {
            get { return _validationMessage; }
            set
            {
                _validationMessage = value;
                OnPropertyChanged();
            }
        }
        private bool? _isValid;
        public bool? IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }
        private bool _isReadonly;
        public bool IsReadonly
        {
            get { return _isReadonly; }
            set
            {
                _isReadonly = value;
                OnPropertyChanged();
            }
        }
    }
}
