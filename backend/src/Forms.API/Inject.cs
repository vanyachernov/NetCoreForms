namespace Forms.API;

public static class Inject
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        
        return services;
    }
}