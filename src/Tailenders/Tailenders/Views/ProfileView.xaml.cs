using Tailenders.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailenders.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentView
    {
        public ProfileView()
        {
            InitializeComponent();
            BindingContext = this.MatchItem;
        }

        public static readonly BindableProperty MatchItemProperty = BindableProperty.Create(
            nameof(MatchItem),
            typeof(MatchItemViewModel),
            typeof(ProfileView),
            default(MatchItemViewModel));

        public MatchItemViewModel MatchItem
        {
            get => (MatchItemViewModel)base.GetValue(MatchItemProperty);
            set => base.SetValue(MatchItemProperty, value);
        }
    }
}