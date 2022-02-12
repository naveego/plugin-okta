using PluginOkta.Helper;

namespace PluginOkta.API.Factory
{
    public interface IApiClientFactory
    {
        IApiClient CreateApiClient(Settings settings);
    }
}