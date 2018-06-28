using Microsoft.Extensions.Configuration;

namespace AppleUsed.Repo
{
    public class DatabaseConfiguration : ConfigurationBase
    {
        private string DataConnectionKey = "appleusedDataConnection";

        private string AuthConnectionKey = "appleusedAuthConnection";

        public string GetDataConnectionString()
        {
            return GetConfiguration().GetConnectionString(DataConnectionKey);
        }
        
        public string GetAuthConnectionString()
        {
            return GetConfiguration().GetConnectionString(AuthConnectionKey);
        }
    }
}