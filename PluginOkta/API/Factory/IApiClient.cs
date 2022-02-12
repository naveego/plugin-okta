using System.Net.Http;
using System.Threading.Tasks;
using PluginOkta.Helper;

namespace PluginOkta.API.Factory
{
    public interface IApiClient
    {
        Task TestConnection();
        Task<HttpResponseMessage> GetAsync(string path);
        Task<Settings> GetSettings();
    }
}