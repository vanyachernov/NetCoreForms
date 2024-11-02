using System.Text;
using CSharpFunctionalExtensions;
using Forms.Application.DTOs;
using Newtonsoft.Json;
using Forms.Application.UserDir.AuthenticateSalesforce;
using Forms.Application.UserDir.СreateAccount;
using Forms.Domain.Shared;

namespace Forms.Application.UserDir.CreateContact;

public class CreateContactHandler
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticateSalesforceHandler _authHandler;

    public CreateContactHandler(
        HttpClient httpClient, 
        AuthenticateSalesforceHandler authHandler)
    {
        _httpClient = httpClient;
        _authHandler = authHandler;
    }
    
    public async Task<Result<string, Error>> Handle(
        string accountId,
        CreateAccountRequest request)
    {
        var token = await _authHandler.GetSalesforceAccessToken();
        
        if (token.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Token");
        }

        var instanceUrl = token.Value.instanceUrl;
        
        var contactData = new
        {
            AccountId = accountId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };
        
        var response = await _httpClient.PostAsync(
            $"{instanceUrl}/services/data/v59.0/sobjects/forms_app/Contact",
            new StringContent(
                JsonConvert.SerializeObject(contactData), 
                Encoding.UTF8,
                "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            return Errors.General.ValueIsInvalid($"Failed to create Contact: {error}");
        }

        var content = await response.Content.ReadAsStringAsync();
        
        var contactResponse = JsonConvert.DeserializeObject<CreateContactResponse>(content);
        
        return contactResponse.Id;
    }
}
