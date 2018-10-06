using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Tailenders.Navigation
{
    public static class NavigationServiceExtensions
    {
        private static ConditionalWeakTable<Page, object> arguments
            = new ConditionalWeakTable<Page, object>();

        public static object GetNavigationArgs(this Page page)
        {
            object argument = null;
            arguments.TryGetValue(page, out argument);

            return argument;
        }

        public static void SetNavigationArgs(this Page page, object args)
            => arguments.Add(page, args);

        public static void ClearAllNavigationArgs()
        {
            arguments = new ConditionalWeakTable<Page, object>();
        }
    }
}