using System.Buffers.Text;
using Microsoft.Extensions.configuration;

namespace AutomationExerciseDemo.Config
{
    public static class ConfigLoader
    {
        private static readonly IConfigurationRoot _config;

        static ConfigLoader()
        {
            _config = new ConfigurationBuilder().AddJsonFile("environments.json").Build();


        }

        public static EnvironmentConfig Load()
        {
            var activeEnv = _config["activeEnvironment"];
            var envSection = _config.GetSection($"environments:{activeEnv}");

            return new EnvironmentConfig
            {
                BaseUrl = envSection["baseUrl"],
                LoginPatch = envSection["loginPath"],
                ApiBaseUrl = envSection["apiBaseUrl"]
            };

        }



        
    }
}