using AddressService.Infrastructure;
using AddressService.IoC;
using AddressService.Repositories;
using AddressService.Repositories.Interfaces;
using AddressService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHttpClient("Address", HttpClient => { HttpClient.BaseAddress = new Uri("https://api.dataforsyningen.dk/");
    HttpClient.Timeout = new TimeSpan(0, 20, 0);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AddressDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration["DbConnection"]);
});
builder.Services.AddServiceRegistry();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.UseAuthorization();

app.MapControllers();

app.Run();
