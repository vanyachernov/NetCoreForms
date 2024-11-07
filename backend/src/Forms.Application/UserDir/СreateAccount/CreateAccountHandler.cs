using System.Net.Http.Headers;
using System.Text;
using CSharpFunctionalExtensions;
using Forms.Application.UserDir.AuthenticateSalesforce;
using Forms.Domain.Shared;
using Newtonsoft.Json;

namespace Forms.Application.UserDir.СreateAccount;

public class CreateAccountHandler
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticateSalesforceHandler _authHandler;

    public CreateAccountHandler(
        HttpClient httpClient, 
        AuthenticateSalesforceHandler authHandler)
    {
        _httpClient = httpClient;
        _authHandler = authHandler;
    }
    
    public async Task<Result<string, Error>> Handle(string accountName)
    {
        var token = await _authHandler.GetSalesforceAccessToken();

        if (token.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Token");
        }

        var accessToken = token.Value.accessToken;
        var instanceUrl = token.Value.instanceUrl;

        _httpClient.DefaultRequestHeaders
            .Authorization = new AuthenticationHeaderValue(
            "Bearer", 
            accessToken);

        var accountData = new
        {
            Name = accountName
        };

        var response = await _httpClient.PostAsync(
            $"{instanceUrl}/services/data/v59.0/sobjects/Account",
            new StringContent(
                JsonConvert.SerializeObject(accountData), 
                Encoding.UTF8, 
                "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            return Errors.Salesforce.Duplicate();
        }

        var content = await response.Content.ReadAsStringAsync();
        
        var accountResponse = JsonConvert.DeserializeObject<CreateAccountResponse>(content);

        return accountResponse?.Id;
    }
}