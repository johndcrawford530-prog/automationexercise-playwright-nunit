
using Microsoft.Extensions.Configuration;
using AutomationExerciseDemo.Config;







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
            var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Environments.json", optional: false, reloadOnChange: true);
            var configRoot = configBuilder.Build();

            // load environment section
            var envSection = configRoot.GetSection("Environment");

            var envConfig = new EnvironmentConfig
            {
                BaseUrl = envSection["BaseUrl"],
                LoginPath = envSection["LoginPath"],
                ApiBaseUrl = envSection["ApiBaseUrl"]
            };

            //load browser section
            var browserSection = configRoot.GetSection("Browser");

            var browserConfig = new BrowserConfig
            {
                Headless = bool.Parse(browserSection["Headless"]?? "false"),
                SlowMo = int.Parse(browserSection["SlowMo"]?? "0")
            };

            envConfig.Browser = browserConfig;

            return envConfig;

        }



        
    }
}