using CSharpFunctionalExtensions;
using DotNetEnv;
using Forms.Domain.Shared;
using Newtonsoft.Json;

namespace Forms.Application.UserDir.AuthenticateSalesforce;

public class AuthenticateSalesforceHandler(HttpClient httpClient)
{
    public async Task<Result<(string accessToken, string instanceUrl), Error>> GetSalesforceAccessToken()
    {
        Env.Load();

        var forceUsername = Environment.GetEnvironmentVariable("SALESFORCE_USER_NAME");
        var forceUserPassword = Environment.GetEnvironmentVariable("SALESFORCE_USER_PASSWORD");
        var forceUserSecretKey = Environment.GetEnvironmentVariable("SALESFORCE_USER_SECRET");
        var forceConsumerKey = Environment.GetEnvironmentVariable("SALESFORCE_CONSUMER_KEY");
        var forceConsumerSecretKey = Environment.GetEnvironmentVariable("SALESFORCE_CONSUMER_SECRET_KEY");

        var tokenResponse = await httpClient.PostAsync(
            "https://login.salesforce.com/services/oauth2/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", forceConsumerKey! },
                { "client_secret", forceConsumerSecretKey! },
                { "username", forceUsername! },
                { "password", forceUserPassword + forceUserSecretKey }
            }));

        if (tokenResponse.IsSuccessStatusCode)
        {
            var json = await tokenResponse.Content.ReadAsStringAsync();
            var tokenData = JsonConvert.DeserializeObject<dynamic>(json);
            var accessToken = tokenData.access_token;
            var instanceUrl = tokenData.instance_url;

            return (accessToken, instanceUrl);
        }

        var errorResponse = await tokenResponse.Content.ReadAsStringAsync();
        
        return Errors.General.ValueIsInvalid($"Failed to retrieve access token: {errorResponse}");
    }

}