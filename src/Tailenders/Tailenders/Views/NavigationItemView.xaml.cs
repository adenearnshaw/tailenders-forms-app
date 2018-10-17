using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailenders.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationItemView : Grid
    {
        public NavigationItemView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(string),
            typeof(NavigationItemView),
            string.Empty,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (NavigationItemView)bindable;
                ctrl.CommandParameter = (string)newValue;
            },
            defaultBindingMode: BindingMode.OneWay);

        private string _commandParameter;

        public string CommandParameter
        {
            get { return _commandParameter; }
            set
            {
                _commandParameter = value;
                OnPropertyChanged();
            }
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(string),
        typeof(NavigationItemView),
        string.Empty,
        propertyChanging: (bindable, oldValue, newValue) =>
        {
            var ctrl = (NavigationItemView)bindable;
            ctrl.Icon = (string)newValue;
        },
        defaultBindingMode: BindingMode.OneWay);

        private string _icon;

        public string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged();
            }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(NavigationItemView),
            string.Empty,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (NavigationItemView)bindable;
                ctrl.Text = (string)newValue;
            },
            defaultBindingMode: BindingMode.OneWay);

        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            "Command",
            typeof(ICommand),
            typeof(NavigationItemView),
            null,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (NavigationItemView)bindable;
                ctrl.Command = (ICommand)newValue;
            },
            defaultBindingMode: BindingMode.OneWay);

        private ICommand _command;

        public ICommand Command
        {
            get { return _command; }
            set
            {
                _command = value;
                OnPropertyChanged();
            }
        }

    }
}