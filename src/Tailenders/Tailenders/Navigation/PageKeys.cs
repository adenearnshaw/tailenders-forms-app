namespace Tailenders.Navigation
{
    public static class PageKeys
    {
        public static string HomePage => PageKey.Home.ToString();
        public static string MatchesPage => PageKey.Matches.ToString();
        public static string ConversationPage => PageKey.Conversation.ToString();

        
        enum PageKey
        {
            Home,
            Matches,
            Conversation
        }
    }
}