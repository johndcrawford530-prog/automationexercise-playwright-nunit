using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;


namespace AutomationExerciseDemo.API.Clients
{
    public abstract class BaseApiClient
    {
        protected readonly HttpClient Client;
        protected readonly EnvironmentConfig Config;


        protected BaseApiClient(EnvironmentConfig config)
        {
            Config = config;
            Client = new HttpClient { BaseAddress = new System.Uri(Config.ApiBaseUrl)};
            
        }


        protected async Task<T?> GetAsync<T>(string endpoint)
        {
            var response = await Client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
           
            return JsonSerializer.Deserialize<T>(json);
        }

        protected async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest body)
        {
           var json = JsonSerializer.Serialize(body);
           var content = new StringContent(json, Encoding.UTF8, "application/json" );
           var response = await Client.PostAsync(endpoint, content);
           response.EnsureSuccessStatusCode();

           var responseJson = await response.Content.ReadAsStringAsync();

           return JsonSerializer.Deserialize<TResponse>(responseJson); 

        }



        protected async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest body)
        {
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(responseJson);

        }


        protected async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await Client.DeleteAsync(endpoint);

            // if reponse returns a 2xx, the delete succeeded
            response.EnsureSuccessStatusCode();
             return response.IsSuccessStatusCode;
        }


    }



}