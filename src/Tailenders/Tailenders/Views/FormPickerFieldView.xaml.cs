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

        public static readonly BindableProperty SelectedOptionProperty = BindableProperty.Create(
            nameof(SelectedOption),
            typeof(EnumPickerOption),
            typeof(FormPickerFieldView),
            default(EnumPickerOption),
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (FormPickerFieldView)bindable;
                ctrl.SelectedOption = (EnumPickerOption)newValue;
            },
            defaultBindingMode: BindingMode.TwoWay);

        public IList<EnumPickerOption> Options
        {
            get => (IList<EnumPickerOption>)base.GetValue(OptionsProperty);
            set => base.SetValue(OptionsProperty, value);
        }

        public EnumPickerOption SelectedOption
        {
            get => (EnumPickerOption)base.GetValue(SelectedOptionProperty);
            set => base.SetValue(SelectedOptionProperty, value);
        }
    }
}
