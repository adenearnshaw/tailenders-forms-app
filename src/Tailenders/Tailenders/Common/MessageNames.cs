namespace Tailenders.Common
{
    public static class MessageNames
    {
        public static string NoPickPhotoSupport => MessageName.NoPickPhotoSupport.ToString();
        public static string NotOldEnough => MessageName.NotOldEnough.ToString();
        public static string ProfileMatch => MessageName.ProfileMatch.ToString();
        public static string SendContactDetails => MessageName.SendContactDetails.ToString();
        public static string Unmatch => MessageName.Unmatch.ToString();
        public static string BlockProfile => MessageName.BlockProfile.ToString();
        public static string ReportProfile => MessageName.ReportProfile.ToString();
        public static string ReloadSearch => MessageName.ReloadSearch.ToString();

        enum MessageName
        {
            NoPickPhotoSupport,
            NotOldEnough,
            ProfileMatch,
            SendContactDetails,
            Unmatch,
            ReportProfile,
            BlockProfile,
            ReloadSearch
        }
    }
}
