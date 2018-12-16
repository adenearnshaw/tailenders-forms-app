using System;
using System.IO;
using System.Threading.Tasks;
using TailendersApi.Client;
using TailendersApi.Contracts;
using Plugin.Media.Abstractions;
using Tailenders.Services;

namespace Tailenders.Managers
{
    public interface IProfileManager 
    {
        Task<Profile> GetUserProfile();
        Task<Profile> SaveUserProfile(Profile unsavedProfile, bool isNewProfile = false);
        Task DeleteUserProfile();
        Task<Profile> UploadProfileImage(MediaFile mediaFile);
        Task RequestPasswordReset();
        Task ReportUser(string profileId, ReportProfileReason reason);
    }

    public class ProfileManager : IProfileManager
    {
        private readonly IProfilesClient _profilesClient;
        private readonly IProfileImageUploader _profileImageUploader;
        private readonly ICredentialsProvider _credentialsProvider;

        private Profile _userProfile;

        public ProfileManager(IProfilesClient profilesClient,
                              IProfileImageUploader profileImageUploader,
                              ICredentialsProvider credentialsProvider)
        {
            _profilesClient = profilesClient;
            _profileImageUploader = profileImageUploader;
            _credentialsProvider = credentialsProvider;
        }

        public async Task<Profile> GetUserProfile()
        {
            if (_userProfile != null)
            {
                return _userProfile;
            }

            var userProfile = await _profilesClient.GetProfile();

            _userProfile = userProfile ?? throw new NullReferenceException();
            return _userProfile;
        }

        public async Task<Profile> SaveUserProfile(Profile unsavedProfile, bool isNewProfile = false)
        {
            Profile updatedProfile = null;
            if (isNewProfile)
            {
                updatedProfile = await _profilesClient.CreateProfile(unsavedProfile);
            }
            else
            {
                updatedProfile = await _profilesClient.UpdateProfile(unsavedProfile);
            }

            _userProfile = updatedProfile;

            return _userProfile;
        }

        public async Task DeleteUserProfile()
        {
            await _profilesClient.DeleteProfile();
            _userProfile = null;
        }

        public async Task<Profile> UploadProfileImage(MediaFile mediaFile)
        {
            try
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(mediaFile.Path)}";
                var result = await _profileImageUploader.UploadImage(fileName, mediaFile.GetStreamWithImageRotatedForExternalStorage());
                _userProfile.Images.Add(result);

                return _userProfile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task RequestPasswordReset()
        {
            await AuthenticationService.Instance.TryResetPassword();
        }

        public async Task ReportUser(string profileId, ReportProfileReason reason)
        {
            await _profilesClient.ReportProfile(profileId, reason);
        }
    }
}
