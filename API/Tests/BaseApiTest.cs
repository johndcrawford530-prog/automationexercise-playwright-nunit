using AutomationExerciseDemo.Config;


namespace AutomationExerciseDemo.API.Tests
{
    public abstract class BaseApiTest
    {
        protected EnvironmentConfig Config;


        public BaseApiTest()
        {
            Config = ConfigLoader.Load();
        }



    }




}