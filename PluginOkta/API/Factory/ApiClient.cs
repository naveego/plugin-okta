using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Naveego.Sdk.Logging;
using PluginOkta.API.Utility;
using PluginOkta.Helper;

namespace PluginOkta.API.Factory
{
    public class ApiClient: IApiClient
    {
        private static HttpClient Client { get; set; }
        private Settings Settings { get; set; }


        public ApiClient(HttpClient client, Settings settings)
        {
            Client = client;
            Settings = settings;
            
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Settings> GetSettings()
        {
            return Settings;
        }
        
        public async Task TestConnection()
        {
            try
            {
                var uriBuilder = new UriBuilder($"{Settings.Domain.TrimEnd('/')}/{Utility.Constants.TestConnectionPath.TrimStart('/')}");
                var uri = new Uri(uriBuilder.ToString());
                
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("SSWS", Settings.ApiKey);
                
                var response = await Client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            try
            {
                var uriBuilder = new UriBuilder($"{path}");
                var uri = new Uri(uriBuilder.ToString());
                
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("SSWS", Settings.ApiKey);
                
                return await Client.SendAsync(request);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string path, StringContent json)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> PutAsync(string path, StringContent json)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> PatchAsync(string path, StringContent json)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            throw new NotImplementedException();
        }
    }
}