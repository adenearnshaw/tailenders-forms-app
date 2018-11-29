using Xamarin.Forms;

namespace Tailenders.Views
{
    public partial class FormEntryFieldView : BaseFormFieldView
    {
        public FormEntryFieldView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            nameof(Keyboard),
            typeof(Keyboard),
            typeof(FormEntryFieldView),
            Keyboard.Default,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormEntryFieldView)bindable;
                ctrl.Keyboard = (Keyboard)newValue;
            },
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty HintProperty = BindableProperty.Create(
            nameof(Hint),
            typeof(string),
            typeof(FormEntryFieldView),
            string.Empty,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormEntryFieldView)bindable;
                ctrl.Hint = (string)newValue;
            },
            defaultBindingMode: BindingMode.OneWay);

        public Keyboard Keyboard
        {
            get => (Keyboard)base.GetValue(KeyboardProperty);
            set => base.SetValue(KeyboardProperty, value);
        }

        public string Hint
        {
            get => (string)base.GetValue(HintProperty);
            set => base.SetValue(HintProperty, value);
        }
    }
}
