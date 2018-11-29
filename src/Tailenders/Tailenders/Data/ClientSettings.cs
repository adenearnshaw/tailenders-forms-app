using TailendersApi.Client;

namespace Tailenders.Data
{
    public class ClientSettings : IClientSettings
    {
        public ClientSettings(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public string BaseUrl { get; }
    }
}
