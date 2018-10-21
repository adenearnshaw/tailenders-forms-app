using Xamarin.Forms;

namespace Tailenders.Views
{
    public partial class FormSwitchFieldView : BaseFormFieldView
    {
        public FormSwitchFieldView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(
            nameof(IsToggled),
            typeof(bool),
            typeof(FormSwitchFieldView),
            false,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormSwitchFieldView)bindable;
                ctrl.IsToggled = (bool)newValue;
            },
            defaultBindingMode: BindingMode.TwoWay);

        private bool _isToggled;
        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                _isToggled = value;
                OnPropertyChanged();
            }
        }
    }
}
