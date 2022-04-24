using System;

namespace Challenge.Services.Config
{
    public class EnvironmentConfigs : IEnvironenmentConfigs
    {
        public string getConnectionString()
        {
            string connectionString = Environment.GetEnvironmentVariable("connectionString");

            if (string.IsNullOrEmpty(connectionString)) {
                return DefaultConfigs.connectionString;
            }

            return connectionString;
        }
    }
}