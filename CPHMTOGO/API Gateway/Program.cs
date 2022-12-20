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
    client.BaseAddress = new Uri(builder.Configuration["ServicesConfiguration:OrderServiceUrl"] ?? throw new InvalidOperationException());
});
builder.Services.AddHttpClient("PaymentService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServicesConfiguration:PaymentServiceUrl"] ?? throw new InvalidOperationException());
});
builder.Services.AddHttpClient("PaymentLoggingService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServicesConfiguration:PaymentLoggingServiceUrl"] ?? throw new InvalidOperationException());
});
builder.Services.AddHttpClient("AddressService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServicesConfiguration:AddressServiceUrl"] ?? throw new InvalidOperationException());
});
builder.Services.AddHttpClient("UserService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServicesConfiguration:UserServiceUrl"] ?? throw new InvalidOperationException());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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