using Forms.API;
using Forms.API.Extensions;
using Forms.Application;
using Forms.Application.Identity.Admin;
using Forms.Application.Identity.Roles;
using Forms.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    using (var scope = app.Services.CreateScope())
    {
        var roleService = scope
            .ServiceProvider
            .GetRequiredService<EnsureRolesHandler>();
        
        await roleService.Handle();
    }
    
    using (var scope = app.Services.CreateScope())
    {
        var adminService = scope
            .ServiceProvider
            .GetRequiredService<EnsureAdminHandler>();
        
        await adminService.Handle();
    }

    app.UseExceptionLogMiddleware();
    
    app.UseAuthentication();

    app.UseAuthorization();
    
    app.MapControllers();
    
    app.Run();
}