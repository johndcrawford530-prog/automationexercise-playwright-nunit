using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;
using System.Data;

namespace AutomationExerciseDemo.UI.Pages
{
    public abstract class BasePage
    {
        protected readonly IPage Page;
        protected readonly EnvironmentConfig Config;

        protected BasePage(IPage page, EnvironmentConfig config)
        {
            Page = page;
            Config = config;
        }

        // navigate to URL:
        protected async Task NavigateToAsync(string path = "")
        {
            //navigate to page
            await Page.GotoAsync($"{Config.BaseUrl}{path}");

            // then remove any ads with close buttons
            await CloseAdsAsync();
            // remove remaining ads with Javascript
            await RemoveAdsUsingJSAsync();
        }

        // Click on element
        protected async Task ClickAsync(string selector)
        {
            //highlight element:
            await HighlightAsync(selector);


            await Page.Locator(selector).ClickAsync();
        }

        // type string
        protected async Task TypeAsync(string selector, string text)
        {
            //highlight element:
            await HighlightAsync(selector);


            await Page.Locator(selector).FillAsync(text);
        }


        // get Text from element
        protected async Task<string> GetTextAsync(string selector)
        {
            //highlight element:
            await HighlightAsync(selector);

            return await Page.Locator(selector).InnerTextAsync();
        }


        // confirm element is visible
        protected async Task<bool> IsVisibleAsync(string selector)
        {
            //highlight element:
            await HighlightAsync(selector);

            return await Page.Locator(selector).IsVisibleAsync();
        }


        // wait for element to be visible
        protected async Task WaitForVisibleAsync(string selector)
        {
            await Page.Locator(selector).WaitForAsync();
        }


        // close any ads if present
        protected async Task CloseAdsAsync()
        {
            var closeButtonSelectors = new[]
            {
                "div[onclick='closeAd()']",
                "button.close",
                "#dismiss-button",
                ".ad-close"
            };


            foreach(var selector in closeButtonSelectors)
            {
                try
                {
                    var locator = Page.Locator(selector);

                    if(await locator.CountAsync() > 0)
                    {
                        await locator.First.ClickAsync(new(){ Force= true});
                    }   
                }
                catch
                {
                    //do nothing
                }
            }

        }

        protected async Task RemoveAdsUsingJSAsync()
        {
            await Page.EvaluateAsync(@"() =>{
                const ads = document.querySelectorAll('iframe, .ad, adsbygoogle');
                ads.forEach(a => a.remove());
            }");
            
        }


        protected async Task HighlightAsync(string selector)
        {
            var element = Page.Locator(selector);

            await element.EvaluateAsync("el=> el.style.border = '3px solid red'");
        }






    }






}