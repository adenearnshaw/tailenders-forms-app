namespace Tailenders.Navigation
{
    public static class PageKeys
    {
        public static string HomePage => PageKey.Home.ToString();
        public static string MatchesPage => PageKey.Matches.ToString();
        public static string ConversationPage => PageKey.Conversation.ToString();
        public static string ProfilePage => PageKey.Profile.ToString();
        public static string SearchSettingsPage => PageKey.SearchSettings.ToString();
        public static string PodcastPage => PageKey.Podcast.ToString();

        
        enum PageKey
        {
            Home,
            Matches,
            Conversation,
            Profile,
            SearchSettings,
            Podcast
        }
    }
}