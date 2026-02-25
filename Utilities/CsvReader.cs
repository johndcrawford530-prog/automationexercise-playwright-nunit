using System.Globalization;
using System.Reflection;
using NUnit.Framework.Interfaces;

namespace AutomationExerciseDemo.Utilities
{
    public static class CsvReader
    {
        
        //read CSV file:
        public static List<T> ReadCsv<T>(String filepath) where T : new()
        {
            var results = new List<T>();
            var lines = File.ReadAllLines(filepath);

            if(lines.Length < 2)
            {
                return results;
            }

            var headers = lines[0].Split(',');

            for(int i =1; i < lines.Length; i++)
            {
                var obj = new T();
                var values = lines[i].Split(',');

                for(int j = 0; j<headers.Length; j++)
                {
                    var prop = typeof(T).GetProperty(
                        headers[i],
                        BindingFlags.Public|BindingFlags.Instance|BindingFlags.IgnoreCase
                    );

                    if (prop != null)
                    {
                        var convertedValue = Convert.ChangeType(
                            values[j],
                            prop.PropertyType,
                            CultureInfo.InvariantCulture
                        );

                        prop.SetValue(obj, convertedValue);
                    }
                }
                results.Add(obj);

            }

            return results;

        }


    }
}