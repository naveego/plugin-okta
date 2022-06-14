using System;

namespace PluginOkta.Helper
{
    public class Settings
    {
        public string ApiKey { get; set; }
        public string Domain { get; set; }
        

        /// <summary>
        /// Validates the settings input object
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new Exception("the ApiKey property must be set");
            }
            if (string.IsNullOrWhiteSpace(Domain))
            {
                throw new Exception("the Domain property must be set");
            }
            if (!Domain.StartsWith("https://") && !Domain.StartsWith("http://"))
            {
                throw new Exception("the Domain property must start with https:// or http://");
            }
        }
    }
}