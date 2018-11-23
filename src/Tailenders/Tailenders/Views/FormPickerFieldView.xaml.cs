using System;
using System.Collections.Generic;
using Tailenders.Common;
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
           typeof(IList<EnumPickerOption>),
           typeof(FormPickerFieldView),
            new List<EnumPickerOption>(),
           propertyChanging: (bindable, oldValue, newValue) =>
           {
               var ctrl = (FormPickerFieldView)bindable;
               ctrl.Options = (IList<EnumPickerOption>)newValue;
           },
           defaultBindingMode: BindingMode.OneWay);


        private IList<EnumPickerOption> _options;
        public IList<EnumPickerOption> Options
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
