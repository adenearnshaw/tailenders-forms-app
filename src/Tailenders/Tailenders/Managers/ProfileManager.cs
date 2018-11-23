using System.Threading.Tasks;
using TailendersApi.Client;
using TailendersApi.Contracts;
using Tailenders.Managers.Exceptions;
using Plugin.Media.Abstractions;

namespace Tailenders.Managers
{
    public interface IProfileManager 
    {
        Task<Profile> GetUserProfile();
        Task<Profile> SaveUserProfile(Profile unsavedProfile, bool isNewProfile = false);
        Task DeleteUserProfile();
        Task<Profile> UploadProfileImage(MediaFile mediaFile);
    }

    public class ProfileManager : IProfileManager
    {
        private readonly IProfilesRetriever _profilesRetriever;
        private readonly IProfileImageUploader _profileImageUploader;

        private Profile _userProfile;

        public ProfileManager(IProfilesRetriever profilesRetriever,
                              IProfileImageUploader profileImageUploader)
        {
            _profilesRetriever = profilesRetriever;
            _profileImageUploader = profileImageUploader;
        }

        public async Task<Profile> GetUserProfile()
        {
            if (_userProfile != null)
            {
                return _userProfile;
            }

            var userProfile = await _profilesRetriever.GetProfile();

            _userProfile = userProfile ?? throw new UserDoesntExistException();
            return _userProfile;
        }

        public async Task<Profile> SaveUserProfile(Profile unsavedProfile, bool isNewProfile = false)
        {
            Profile updatedProfile = null;
            if (isNewProfile)
            {
                updatedProfile = await _profilesRetriever.CreateProfile(unsavedProfile);
            }
            else
            {
                updatedProfile = await _profilesRetriever.UpdateProfile(unsavedProfile);
            }

            _userProfile = updatedProfile;

            return _userProfile;
        }

        public async Task DeleteUserProfile()
        {
            await _profilesRetriever.DeleteProfile();
            _userProfile = null;
        }

        public async Task<Profile> UploadProfileImage(MediaFile mediaFile)
        {
            var result = await _profileImageUploader.UploadImage(mediaFile.GetStreamWithImageRotatedForExternalStorage());
            _userProfile.Images.Add(result);

            return _userProfile;
        }
    }
}
