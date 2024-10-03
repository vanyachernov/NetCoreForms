using Forms.API;
using Forms.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddInfrastructure();

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
}