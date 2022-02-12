using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Naveego.Sdk.Logging;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PluginOkta.API.Factory;
using PluginOkta.DataContracts;

namespace PluginOkta.API.Utility.EndpointHelperEndpoints
{
    public class ContactsEndpointHelper
    {
        private class UsersEndpoints : Endpoint
        {
            public override async Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
            {
                List<string> staticSchemaProperties = new List<string>()
                {
                    "id",
                    "status",
                    "created",
                    "activated",
                    "statusChanged",
                    "lastLogin",
                    "lastUpdated",
                    "passwordChanged",
                    "type.id",
                    "profile.firstName",
                    "profile.lastName",
                    "profile.mobilePhone",
                    "profile.secondEmail",
                    "profile.login",
                    "profile.email",
                    "credentials.provider.type",
                    "credentials.provider.name",
                    "_links.self.href",
                };

                var properties = new List<Property>();

                foreach (var staticProperty in staticSchemaProperties)
                {
                    var property = new Property();

                    property.Id = staticProperty;
                    property.Name = staticProperty;

                    switch (staticProperty)
                    {
                        case ("id"):
                            property.IsKey = true;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                        default:
                            property.IsKey = false;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                    }

                    properties.Add(property);
                }

                schema.Properties.Clear();
                schema.Properties.AddRange(properties);

                schema.DataFlowDirection = GetDataFlowDirection();

                return schema;
            }
            public override async IAsyncEnumerable<Record> ReadRecordsAsync(IApiClient apiClient, Schema schema, bool isDiscoverRead = false)
            {
                var after = "";
                var hasMore = true;
                var settings = await apiClient.GetSettings();
                do
                {
                    var response = await apiClient.GetAsync($"{settings.Domain.TrimEnd('/')}{QueryPath}{after}");

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var errorResponseWrapper =
                            JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync());
                        if (errorResponseWrapper.ErrorSummary == "Invalid paging request.")
                        {
                            hasMore = false;
                            continue;
                        }
                    }
                    
                    response.EnsureSuccessStatusCode();

                    var userResponseWrapper =
                        JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());

                    if (userResponseWrapper?.Count == 0)
                    {
                        hasMore = false;
                        continue;
                    }

                    foreach (var user in userResponseWrapper)
                    {
                        after = $"&after={user.Id}";
                        var recordMap = new Dictionary<string, string>();

                        recordMap["id"] = user.Id;
                        recordMap["status"] = user.Status;
                        recordMap["created"] = user.Created;
                        recordMap["activated"] = user.Activated;
                        recordMap["statusChanged"] = user.StatusChanged;
                        recordMap["lastLogin"] = user.LastLogin;
                        recordMap["lastUpdated"] = user.LastUpdated;
                        recordMap["passwordChanged"] = user.PasswordChanged;
                        recordMap["type.id"] = user.Type.Id;
                        recordMap["profile.firstName"] = user.Profile.FirstName;
                        recordMap["profile.lastName"] = user.Profile.LastName;
                        recordMap["profile.mobilePhone"] = user.Profile.MobilePhone;
                        recordMap["profile.secondEmail"] = user.Profile.SecondEmail;
                        recordMap["profile.login"] = user.Profile.Login;
                        recordMap["profile.email"] = user.Profile.Email;
                        recordMap["credentials.provider.type"] = user.Credentials.Provider.Type;
                        recordMap["credentials.provider.name"] = user.Credentials.Provider.Name;
                        recordMap["_links.self.href"] = user.Links.Self.Href;
                        
                        yield return new Record
                        {
                            Action = Record.Types.Action.Upsert,
                            DataJson = JsonConvert.SerializeObject(recordMap)
                        };
                    }
                } while (hasMore);
            }
        }
        private class UserEmailsEndpoints : Endpoint
        {
            public override async Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
            {
                List<string> staticSchemaProperties = new List<string>()
                {
                    "id",
                    "email.id",
                    "email.status",
                    "email.type",
                    "email.value"
                };

                var properties = new List<Property>();

                foreach (var staticProperty in staticSchemaProperties)
                {
                    var property = new Property();

                    property.Id = staticProperty;
                    property.Name = staticProperty;

                    switch (staticProperty)
                    {
                        case ("id"):
                        case ("email.id"):
                            property.IsKey = true;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                        default:
                            property.IsKey = false;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                    }

                    properties.Add(property);
                }

                schema.Properties.Clear();
                schema.Properties.AddRange(properties);

                schema.DataFlowDirection = GetDataFlowDirection();

                return schema;
            }
            public override async IAsyncEnumerable<Record> ReadRecordsAsync(IApiClient apiClient, Schema schema, bool isDiscoverRead = false)
            {
                var after = "";
                var hasMore = true;
                var settings = await apiClient.GetSettings();
                do
                {
                    var response = await apiClient.GetAsync($"{settings.Domain.TrimEnd('/')}{QueryPath}{after}");

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var errorResponseWrapper =
                            JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync());
                        if (errorResponseWrapper.ErrorSummary == "Invalid paging request.")
                        {
                            hasMore = false;
                            continue;
                        }
                    }
                    
                    response.EnsureSuccessStatusCode();

                    var userResponseWrapper =
                        JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());

                    if (userResponseWrapper?.Count == 0)
                    {
                        hasMore = false;
                        continue;
                    }

                    foreach (var user in userResponseWrapper)
                    {
                        after = $"&after={user.Id}";
                        var emailId = 0;
                        foreach (var email in user.Credentials.Emails)
                        {
                            var recordMap = new Dictionary<string, string>();

                            recordMap["id"] = user.Id;
                            recordMap["email.id"] = emailId.ToString();
                            recordMap["email.status"] = email.Status;
                            recordMap["email.type"] = email.Type;
                            recordMap["email.value"] = email.Value;

                            emailId++;
                            yield return new Record
                            {
                                Action = Record.Types.Action.Upsert,
                                DataJson = JsonConvert.SerializeObject(recordMap)
                            };
                        }
                    }
                } while (hasMore);
            }
        }
        public static readonly Dictionary<string, Endpoint> ContactsEndpoints = new Dictionary<string, Endpoint>
        {
            {
                "AllUsers", new UsersEndpoints
                {
                    Id = "AllUsers",
                    Name = "All Users",
                    QueryPath = "/api/v1/users?limit=200",
                    SupportedActions = new List<EndpointActions>
                    {
                        EndpointActions.Get
                    },
                    ShouldGetStaticSchema = true
                }
            },
            {
                "UserEmails", new UserEmailsEndpoints
                {
                    Id = "UserEmails",
                    Name = "User Emails",
                    QueryPath = "/api/v1/users?limit=200",
                    SupportedActions = new List<EndpointActions>
                    {
                        EndpointActions.Get
                    },
                    ShouldGetStaticSchema = true
                }
            }
        };
    }
}