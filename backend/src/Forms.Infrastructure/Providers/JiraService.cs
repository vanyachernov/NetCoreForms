using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CSharpFunctionalExtensions;
using DotNetEnv;
using Forms.Application.JiraDir;
using Forms.Application.JiraDir.CreateTicket;
using Forms.Application.JiraDir.SearchTickets;
using Forms.Domain.Shared;

namespace Forms.Infrastructure.Providers;

public class JiraService : IJiraService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _apiToken;
    private readonly string _userEmail;

    public JiraService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
        Env.Load("../Forms.API/.env");

        _baseUrl = Environment.GetEnvironmentVariable("JIRA_BASE_URL")!;
        _apiToken = Environment.GetEnvironmentVariable("JIRA_API_TOKEN")!;
        _userEmail = Environment.GetEnvironmentVariable("JIRA_USER_EMAIL")!;

        _httpClient.BaseAddress = new Uri(_baseUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
            Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_userEmail}:{_apiToken}")));
    }
    
    public async Task<Result<CreateTicketResponse, Error>> CreateTicketAsync(
        CreateTicketRequest request,
        string userEmail,
        string displayName)
    {
        var userExists = await UserExistsAsync(userEmail);

        if (userExists.IsFailure)
        {
            Errors.General.ValueIsInvalid("Check user existing");
        }

        if (!userExists.Value)
        {
            var userCreated = await CreateUserAsync(
                displayName, 
                userEmail);
            
            if (userCreated.IsFailure)
            {
                Errors.General.ValueIsInvalid("Creating user");
            }
        }
        
        var response = await _httpClient.PostAsJsonAsync(
            $"{_baseUrl}/rest/api/2/issue", 
            request);
        
        response.EnsureSuccessStatusCode();
        
        var jiraTicketResponse = await response.Content
            .ReadFromJsonAsync<CreateTicketResponse>();  
    
        return jiraTicketResponse;
    }

    public async Task<List<CreateTicketResponse>> GetUserTicketsAsync(string userId)
    {
        var jqlQuery = $"reporter={userId}";
        var url = $"/rest/api/2/search?jql={Uri.EscapeDataString(jqlQuery)}";
        
        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var searchResult = await response.Content.ReadFromJsonAsync<SearchTicketsResponse>();

        return searchResult?.Issues ?? new List<CreateTicketResponse>();
    }

    public async Task<Result<bool, Error>> CreateUserAsync(
        string displayName, 
        string email)
    {
        var newUser = new
        {
            displayName,
            emailAddress = email,
            name = email,
            notification = "Jira",
            active = true,
            applicationKeys = new string[] { "jira-software" }
        };
    
        var requestUri = "rest/api/2/user";
        
        var requestContent = new StringContent(
            JsonSerializer.Serialize(newUser), 
            Encoding.UTF8, 
            "application/json");
        
        var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
        {
            Content = requestContent
        };

        var authToken = Convert.ToBase64String(
            Encoding.ASCII.GetBytes($"{_userEmail}:{_apiToken}"));
        
        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Basic", 
            authToken);

        try
        {
            var response = await _httpClient.SendAsync(request);
            
            response.EnsureSuccessStatusCode();
            
            return true;
        }
        catch (HttpRequestException)
        { 
            Errors.General.ValueIsInvalid("Creating user");
            return false;
        }
    }
    
    public async Task<Result<bool, Error>> UserExistsAsync(string email)
    {
        var requestUri = $"rest/api/2/user?username={email}";
        
        var request = new HttpRequestMessage(
            HttpMethod.Get, 
            requestUri);
        
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        var authToken = Convert.ToBase64String(
            Encoding.ASCII.GetBytes($"{_userEmail}:{_apiToken}"));
        
        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Basic", 
            authToken);

        try
        {
            var response = await _httpClient.SendAsync(request);
        
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        
            response.EnsureSuccessStatusCode();
            
            return true;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }
}