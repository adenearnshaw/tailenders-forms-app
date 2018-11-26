using Xamarin.Forms;

namespace Tailenders.Views
{
    public partial class FormEditorFieldView : BaseFormFieldView
    {
        public FormEditorFieldView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty HintProperty = BindableProperty.Create(
            nameof(Hint),
            typeof(string),
            typeof(FormEditorFieldView),
            string.Empty,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormEditorFieldView)bindable;
                ctrl.Hint = (string)newValue;
            },
            defaultBindingMode: BindingMode.OneWay);

        public string Hint
        {
            get => (string)base.GetValue(HintProperty);
            set => base.SetValue(HintProperty, value);
        }
    }
}
