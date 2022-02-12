using System.Collections.Generic;
using Newtonsoft.Json;

namespace PluginOkta.DataContracts
{

     public partial class User
     {
         [JsonProperty("id")]
         public string Id { get; set; }

         [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
         public string Status { get; set; }

         [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
         public string Created { get; set; }

         [JsonProperty("activated", NullValueHandling = NullValueHandling.Ignore)]
         public string Activated { get; set; }

         [JsonProperty("statusChanged", NullValueHandling = NullValueHandling.Ignore)]
         public string StatusChanged { get; set; }

         [JsonProperty("lastLogin", NullValueHandling = NullValueHandling.Ignore)]
         public string LastLogin { get; set; }

         [JsonProperty("lastUpdated", NullValueHandling = NullValueHandling.Ignore)]
         public string LastUpdated { get; set; }

         [JsonProperty("passwordChanged", NullValueHandling = NullValueHandling.Ignore)]
         public string PasswordChanged { get; set; }

         [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
         public TypeClass Type { get; set; }

         [JsonProperty("profile", NullValueHandling = NullValueHandling.Ignore)]
         public Profile Profile { get; set; }

         [JsonProperty("credentials", NullValueHandling = NullValueHandling.Ignore)]
         public Credentials Credentials { get; set; }

         [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
         public Links Links { get; set; }
     }
    public partial class Credentials
    {
        [JsonProperty("emails", NullValueHandling = NullValueHandling.Ignore)]
        public Email[] Emails { get; set; }

        [JsonProperty("provider", NullValueHandling = NullValueHandling.Ignore)]
        public Provider Provider { get; set; }
    }

    public partial class Email
    {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }

    public partial class Provider
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("self", NullValueHandling = NullValueHandling.Ignore)]
        public Self Self { get; set; }
    }

    public partial class Self
    {
        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        public string Href { get; set; }
    }

    public partial class Profile
    {
        [JsonProperty("firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("mobilePhone", NullValueHandling = NullValueHandling.Ignore)]
        public string MobilePhone { get; set; }

        [JsonProperty("secondEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string SecondEmail { get; set; }

        [JsonProperty("login", NullValueHandling = NullValueHandling.Ignore)]
        public string Login { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
    }

    public partial class TypeClass
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
    }
    
    public partial class ErrorResponse
    {
        [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; }

        [JsonProperty("errorSummary", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorSummary { get; set; }

        [JsonProperty("errorLink", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorLink { get; set; }

        [JsonProperty("errorId", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorId { get; set; }

        [JsonProperty("errorCauses", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorCause[] ErrorCauses { get; set; }
    }

    public partial class ErrorCause
    {
        [JsonProperty("errorSummary", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorSummary { get; set; }
    }
}