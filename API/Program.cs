using API.Business;
using API.Dto;
using Business;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IConfiguration configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IConnectionMultiplexer>(c=>
{
    var options=ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));

    return ConnectionMultiplexer.Connect(options);
}
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<INebimIntegrationService,NebimIntegrationManager>();
builder.Services.AddScoped<IBasketRepository,BasketRepository>();

builder.Services.Configure<ConnectRequestModel>(builder.Configuration.GetSection("ConnectRequestModel"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder => builder.WithOrigins("https://localhost:4200/"
    ));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.WithOrigins("https://localhost:4200/").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
