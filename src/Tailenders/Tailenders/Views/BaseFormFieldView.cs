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

        public string Value
        {
            get => (string)base.GetValue(ValueProperty);
            set => base.SetValue(ValueProperty, value);
        }

        public string Label
        {
            get => (string)base.GetValue(LabelProperty);
            set => base.SetValue(LabelProperty, value);
        }

        public string ValidationMessage
        {
            get => (string)base.GetValue(ValidationMessageProperty);
            set => base.SetValue(ValidationMessageProperty, value);
        }

        public bool? IsValid
        {
            get => (bool?)base.GetValue(IsValidProperty);
            set => base.SetValue(IsValidProperty, value);
        }

        public bool IsReadonly
        {
            get => (bool)base.GetValue(IsReadonlyProperty);
            set => base.SetValue(IsReadonlyProperty, value);
        }
    }
}
