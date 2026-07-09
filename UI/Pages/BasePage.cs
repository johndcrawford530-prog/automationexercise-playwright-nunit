using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;
using System.Data;
using System.Text.RegularExpressions;

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

            //close any Google Vignette ads:
            await CloseGoogleVignetteAsync();
            // then remove any ads with close buttons
            await CloseAdsAsync();
            // remove remaining ads with Javascript
            await RemoveAdsUsingJSAsync();
        }

        // Click on element
        protected async Task ClickAsync(string selector)
        {
            //highlight element:
          //  await HighlightAsync(selector);

           // await WaitForVisibleAsync(selector);


            await Page.Locator(selector).ClickAsync();
        }

        // type string
        protected async Task TypeAsync(string selector, string text)
        {
            //highlight element:
          //  await HighlightAsync(selector);


            await Page.Locator(selector).FillAsync(text);
        }


        // get Text from element
        protected async Task<string> GetTextAsync(string selector)
        {
            //highlight element:
           // await HighlightAsync(selector);

            return await Page.Locator(selector).InnerTextAsync();
        }


        // confirm element is visible
        protected async Task<bool> IsVisibleAsync(string selector)
        {
            //highlight element:
            //await HighlightAsync(selector);

            return await Page.Locator(selector).IsVisibleAsync();
        }


        // wait for element to be visible
        protected async Task WaitForVisibleAsync(string selector)
        {
            await Page.WaitForSelectorAsync(selector, new() 
                { 
                    State = WaitForSelectorState.Visible 
                });


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



        // remove the full page Google Vignette Ads that randomly break tests
        protected async Task CloseGoogleVignetteAsync()
        {
            // Look for the vignette iframe
            var vignetteFrame = Page.FrameByUrl(new Regex("google_vignette"));

            if (vignetteFrame != null)
            {
                // Try to click the close button inside the iframe
                var closeButton = vignetteFrame.Locator("div[role='button'], button, .close");

                if (await closeButton.CountAsync() > 0)
                {
                    try
                    {
                        await closeButton.First.ClickAsync(new() { Force = true });
                    }
                    catch { /* ignore */ }
                }

                // Remove the iframe entirely as a fallback
                await Page.EvaluateAsync(@"() => {
                    document.querySelectorAll('iframe').forEach(el => {
                        if (el.src.includes('google_vignette')) el.remove();
                    });
                }");
            }
        }


        // work around for google vignette URL issue:
        public async Task GoogleVignetteFixAsync(String expectedUrl)
        {
            var url = Page.Url;

            if (url.Contains("google_vignette"))
            {
                await Page.GotoAsync(expectedUrl);
                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                return;
            }

            // if a google vignette frame is displayed:

            var vignetteFrame = Page.Frames.FirstOrDefault(f => f.Url.Contains("googleads"));

            if(vignetteFrame != null)
            {

                await Page.GotoAsync(expectedUrl);
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                
            }

        }



        //helper method to remove any ads that may impact tests:
        protected async Task ClearAdsAsync()
        {
            //close any Google Vignette ads:
            await CloseGoogleVignetteAsync();
            // then remove any ads with close buttons
            await CloseAdsAsync();
            // remove remaining ads with Javascript
            await RemoveAdsUsingJSAsync();
        
        }



        protected async Task HighlightAsync(string selector)
        {
            var element = Page.Locator(selector);

            await element.EvaluateAsync("el=> el.style.border = '3px solid red'");
        }






    }






}