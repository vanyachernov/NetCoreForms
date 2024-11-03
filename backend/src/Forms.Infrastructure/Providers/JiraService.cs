using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CSharpFunctionalExtensions;
using DotNetEnv;
using Forms.Application.JiraDir;
using Forms.Application.JiraDir.CreateTicket;
using Forms.Application.JiraDir.GetUser;
using Forms.Application.JiraDir.SearchTickets;
using Forms.Application.JiraDir.Shared;
using Forms.Domain.Shared;

namespace Forms.Infrastructure.Providers;

public class JiraService : IJiraService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _apiToken;
    private readonly string _userEmail;
    private readonly string _projectId;

    public JiraService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
        Env.Load("../Forms.API/.env");

        _baseUrl = Environment.GetEnvironmentVariable("JIRA_BASE_URL")!;
        _apiToken = Environment.GetEnvironmentVariable("JIRA_API_TOKEN")!;
        _userEmail = Environment.GetEnvironmentVariable("JIRA_USER_EMAIL")!;
        _projectId = Environment.GetEnvironmentVariable("JIRA_PROJECT_ID")!;

        _httpClient.BaseAddress = new Uri(_baseUrl);
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
            Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_userEmail}:{_apiToken}")));
    }
    
    public async Task<Result<CreateTicketResponse, Error>> CreateTicketAsync(CreateTicketRequest request)
    {
        var userEmail = request.Email;

        var userExists = await UserExistsAsync(userEmail);
        
        if (userExists.IsFailure)
        {
            return userExists.Error;
        }
        
        var accountId = string.Empty;

        if (!userExists.Value)
        {
            var userCreatedResult = await CreateUserAsync(userEmail);
            
            if (userCreatedResult.IsFailure)
            {
                return userCreatedResult.Error;
            }

            accountId = userCreatedResult.Value;
        }
        else
        {
            var existingUserAccountIdResult = await GetUserAccountIdAsync(userEmail);
        
            if (existingUserAccountIdResult.IsFailure)
            {
                return existingUserAccountIdResult.Error;
            }

            accountId = existingUserAccountIdResult.Value;
        }
        
        var jiraIssueRequest = new JiraIssueRequest
        {
            fields = new Fields
            {
                project = new Project
                {
                    key = _projectId
                },
                summary = request.Summary,
                description = $"{request.Description}\nTask Site: {request.CurrentUrl}",
                issuetype = new IssueType
                {
                    id = "10001"
                },
                assignee = new Assigne
                {
                    accountId = accountId
                }
            }
        };
        
        var content = new StringContent(
            JsonSerializer.Serialize(jiraIssueRequest), 
            Encoding.UTF8, 
            "application/json");
        
        var response = await _httpClient
            .PostAsync(
                $"{_baseUrl}/rest/api/2/issue", 
                content);
        
        try
        {
            response.EnsureSuccessStatusCode();
            
            var jiraTicketResponse = await response.Content.ReadFromJsonAsync<CreateTicketResponse>();
            
            return jiraTicketResponse;
        }
        catch (HttpRequestException ex)
        {
            return Errors.General.ValueIsInvalid($"Failed to create ticket: {ex.Message}");
        }
    }

    public async Task<List<CreateTicketResponse>> GetUserTicketsAsync(string userId)
    {
        var jqlQuery = $"reporter={userId}";
        
        var url = $"/rest/api/2/search?jql={Uri.EscapeDataString(jqlQuery)}";
        
        var response = await _httpClient.GetAsync(url);
        
        response.EnsureSuccessStatusCode();

        var searchResult = await response.Content.ReadFromJsonAsync<SearchTicketsResponse>();
        
        return searchResult?.Issues ?? [];
    }
    
    public async Task<Result<string, Error>> CreateUserAsync(string email)
    {
        var userShortname = email[..email.IndexOf('@')];
    
        var newUser = new
        {
            displayName = userShortname,
            emailAddress = email,
            name = userShortname,
            password = Guid.NewGuid().ToString("N").Substring(0, 12),
            products = new[] { "jira-software" }
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

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        try
        {
            var response = await _httpClient.SendAsync(request);
        
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseContent);
            var accountId = jsonDocument.RootElement.GetProperty("accountId").GetString();
        
            return accountId;
        }
        catch (HttpRequestException)
        { 
            return Errors.General.ValueIsInvalid("Creating user");
        }
    }
    
    public async Task<Result<bool, Error>> UserExistsAsync(string email)
    {
        var requestUri = $"rest/api/2/user/search?query={email}";
        
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        try
        {
            var response = await _httpClient.SendAsync(request);
            
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }
            
                var errorContent = await response.Content.ReadAsStringAsync();
                
                return Errors.General.ValueIsInvalid($"Failed to check user existence: {errorContent}");
            }

            var users = await response.Content.ReadFromJsonAsync<List<dynamic>>();
            
            return users != null && users.Count > 0;
        }
        catch (HttpRequestException ex)
        {
            return Errors.General.ValueIsInvalid($"User existence check failed: {ex.Message}");
        }
    }
    
    public async Task<Result<string, Error>> GetUserAccountIdAsync(string email)
    {
        var requestUri = $"rest/api/2/user/search?query={email}";

        try
        {
            var response = await _httpClient.GetAsync(requestUri);
        
            response.EnsureSuccessStatusCode();

            var users = await response.Content.ReadFromJsonAsync<List<GetUserResponse>>();

            var user = users?.FirstOrDefault();
        
            if (user == null)
            {
                return Errors.General.ValueIsInvalid("User not found");
            }

            return user.accountId;
        }
        catch (HttpRequestException)
        {
            return Errors.General.ValueIsInvalid("Error fetching user accountId");
        }
    }
}