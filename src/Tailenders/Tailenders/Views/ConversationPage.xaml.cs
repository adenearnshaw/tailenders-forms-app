using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tailenders.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Tailenders.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConversationPage : ContentPage
	{
        ConversationPageViewModel vm;
        public ConversationPage ()
		{
			InitializeComponent ();
            vm = BindingContext as ConversationPageViewModel;

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var x = MessageListView.ItemsSource;
            MessageListView.ScrollTo(vm.Messages.Last(), ScrollToPosition.MakeVisible, false);
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            MessageListView.ScrollTo(vm.Messages.Last(), ScrollToPosition.End, false);
            //EntryText.Focus();
        }

        private void EntryText_Focused(object sender, FocusEventArgs e)
        {
            MessageListView.ScrollTo(vm.Messages.Last(), ScrollToPosition.End, false);
        }
    }
}