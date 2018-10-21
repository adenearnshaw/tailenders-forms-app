using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tailenders.Views
{
    public partial class FormPickerFieldView : BaseFormFieldView
    {
        public FormPickerFieldView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty OptionsProperty = BindableProperty.Create(
           nameof(Options),
           typeof(IList<string>),
           typeof(FormPickerFieldView),
            new List<string>(),
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (FormPickerFieldView)bindable;
               ctrl.Options = (IList<string>)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);


        private IList<string> _options;
        public IList<string> Options
        {
            get { return _options; }
            set
            {
                _options = value;
                OnPropertyChanged();
            }
        }
    }
}
