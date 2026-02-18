using Microsoft.Playwright;
using System.Threading.Tasks;

namespace AutomationExerciseDemo.UI.Models{

public class ProductItem
{
    
    public string Name{get; set;}
    public string Price{get; set;}

    public ILocator AddToCartButton{get; set;}

    public ILocator ViewDetailsLink{get; set;}

    public ILocator RootElement{get; set;}

    public async Task ClickAddToCartAsync()=> await AddToCartButton.ClickAsync();

    public async Task ClickViewDetailsAsync()=> await ViewDetailsLink.ClickAsync();



}
}