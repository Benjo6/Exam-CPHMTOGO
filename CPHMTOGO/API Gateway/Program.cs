using AuthenticationService;
using service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddGrpcClient<AuthenticationActivity.AuthenticationActivityClient>(options =>
{
    options.Address = new Uri(builder.Configuration["ServicesConfiguration:AuthenticationServiceUrl"] ?? throw new InvalidOperationException());
});
builder.Services.AddHttpClient("OrderService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["DbConnection"] ?? throw new InvalidOperationException());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();