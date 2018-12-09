namespace Tailenders.Navigation
{
    public static class PageKeys
    {
        public static string AboutPage => PageKey.About.ToString();
        public static string ConversationPage => PageKey.Conversation.ToString();
        public static string DeleteProfilePage => PageKey.DeleteProfile.ToString();
        public static string ErrorPage => PageKey.Error.ToString();
        public static string HomePage => PageKey.Home.ToString();
        public static string LoginPage => PageKey.Login.ToString();
        public static string MasterPage => PageKey.Master.ToString();
        public static string MatchDetailPage => PageKey.MatchDetail.ToString();
        public static string MatchesPage => PageKey.Matches.ToString();
        public static string NewProfilePage => PageKey.NewProfile.ToString();
        public static string PodcastPage => PageKey.Podcast.ToString();
        public static string ProfilePage => PageKey.Profile.ToString();
        public static string SearchSettingsPage => PageKey.SearchSettings.ToString();


        
        enum PageKey
        {
            About,
            Conversation,
            DeleteProfile,
            Error,
            Home,
            Login,
            Master,
            MatchDetail,
            Matches,
            NewProfile,
            Podcast,
            Profile,
            SearchSettings,
            
        }
    }
}