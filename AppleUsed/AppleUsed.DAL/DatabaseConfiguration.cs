using Microsoft.Extensions.Configuration;

namespace AppleUsed.DAL
{
    public class DatabaseConfiguration : ConfigurationBase
    {
        private string DataConnectionKey = "appleusedDataConnection";

        public string GetDataConnectionString()
        {
            return GetConfiguration().GetConnectionString(DataConnectionKey);
        }
    }
}