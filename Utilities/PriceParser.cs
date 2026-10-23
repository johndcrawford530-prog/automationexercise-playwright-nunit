
namespace AutomationExerciseDemo.Utilities
{


    public static class PriceParser
    {



        // parse price data:
            public static decimal Parse(string text)
            {
                return decimal.Parse(text.Replace("Rs.", "").Trim());
            }

        
    }
}




  