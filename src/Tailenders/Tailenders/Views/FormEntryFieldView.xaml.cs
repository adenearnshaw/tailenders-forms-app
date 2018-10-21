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

        private Keyboard _keyboard;
        public Keyboard Keyboard
        {
            get { return _keyboard; }
            set
            {
                _keyboard = value;
                OnPropertyChanged();
            }
        }
    }
}
