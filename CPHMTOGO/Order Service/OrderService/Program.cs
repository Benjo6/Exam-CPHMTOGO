using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure;
using OrderService.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.EnableEndpointRouting = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RepositoryContext>(option =>
{
    option.UseNpgsql(builder.Configuration["DbConnection"]);
});

builder.Services.AddModelRegistry();
builder.Services.AddServicesRegistry();
builder.Services.AddHttpClient("order", client =>
{
    client.BaseAddress = new Uri("api/");

});


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Cors Register
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();