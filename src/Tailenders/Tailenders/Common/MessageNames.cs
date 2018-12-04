namespace Tailenders.Common
{
    public static class MessageNames
    {
        public static string NoPickPhotoSupport => MessageName.NoPickPhotoSupport.ToString();
        public static string NotOldEnough => MessageName.NotOldEnough.ToString();
        public static string ProfileMatch => MessageName.ProfileMatch.ToString();

        enum MessageName
        {
            NoPickPhotoSupport,
            NotOldEnough,
            ProfileMatch
        }
    }
}
