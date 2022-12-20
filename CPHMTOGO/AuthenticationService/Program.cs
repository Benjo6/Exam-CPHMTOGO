using AuthenticationService;
using AuthenticationService.Application;
using AuthenticationService.Infrastructure;
using Microsoft.EntityFrameworkCore;

class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpc();
        builder.Services.AddAutoMapper(typeof(ServiceProfile));
        builder.Services.AddScoped<IAuthenticationApplicationRepository, AuthenticationApplicationRepository>();
        builder.Services.AddDbContext<AuthenticationDbContext>(option =>
        {
            option.UseNpgsql(builder.Configuration["DbConnection"] ?? throw new InvalidOperationException());
        });

        var app = builder.Build();

        app.MapGrpcService<AuthenticationService.Service.AuthenticationService>();
        app.MapGet("/",
            () =>
                "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


        app.Run();
        
    }
}