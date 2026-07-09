using System;
using System.Collections.Generic;
using AutomationExerciseDemo.UI.Models;



public static class SearchRelevanceHelper
{
    public static bool IsRelevant(ProductItem product, IEnumerable<string> keywords)
    {
        var name = product.Name.ToLower();
        

        return keywords.Any(k =>
        {
            var key = k.ToLower();
            return name.Contains(key);
        });
    }
}