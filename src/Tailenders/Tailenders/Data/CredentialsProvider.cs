using TailendersApi.Client;

namespace Tailenders.Data
{
    public class CredentialsProvider : ICredentialsProvider
    {
        public string UserId { get; private set; }
        public string AuthenticationToken { get; private set; }

        public void UpdateCredentials(string userId, string authenticationToken)
        {
            UserId = userId;
            AuthenticationToken = authenticationToken;
        }
    }
}
