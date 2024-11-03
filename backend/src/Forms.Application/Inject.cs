using Forms.Application.Identity.Admin;
using Forms.Application.Identity.Roles;
using Forms.Application.TemplateDir.AddQuestion;
using Forms.Application.TemplateDir.AddUserAccessToTemplate;
using Forms.Application.TemplateDir.CreateTemplate;
using Forms.Application.TemplateDir.CreateUser;
using Forms.Application.TemplateDir.DeleteTemplate;
using Forms.Application.TemplateDir.GetQuestions;
using Forms.Application.TemplateDir.GetTemplate;
using Forms.Application.TemplateDir.GetTemplates;
using Forms.Application.TemplateDir.GetUserTemplates;
using Forms.Application.UserDir.AuthenticateSalesforce;
using Forms.Application.UserDir.CreateContact;
using Forms.Application.UserDir.GetUsers;
using Forms.Application.UserDir.СreateAccount;
using Microsoft.Extensions.DependencyInjection;

namespace Forms.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<EnsureAdminHandler>();
        
        services.AddScoped<EnsureRolesHandler>();

        services.AddScoped<AddQuestionHandler>();
        
        services.AddScoped<GetUsersHandler>();
        
        services.AddScoped<CreateTemplateHandler>();
        
        services.AddScoped<CreateUserHandler>();
        
        services.AddScoped<AuthenticateSalesforceHandler>();
        
        services.AddScoped<CreateAccountHandler>();

        services.AddScoped<CreateContactHandler>();
        
        services.AddScoped<DeleteTemplateHandler>();
        
        services.AddScoped<GetTemplatesHandler>();
        
        services.AddScoped<GetTemplateHandler>();
        
        services.AddScoped<GetQuestionsHandler>();
        
        services.AddScoped<GetUserTemplatesHandler>();
        
        services.AddScoped<AddUserAccessToTemplateHandler>();
        
        services.AddHttpClient<CreateContactHandler>();
            
        return services;
    }
}