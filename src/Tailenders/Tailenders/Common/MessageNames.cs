namespace Tailenders.Common
{
    public static class MessageNames
    {
        public static string NoPickPhotoSupport => MessageName.NoPickPhotoSupport.ToString();
        public static string NotOldEnough => MessageName.NotOldEnough.ToString();

        enum MessageName
        {
            NoPickPhotoSupport,
            NotOldEnough
        }
    }
}
