using WebApi.RabbitMQ;
using WebApi.Services;
using WebApi.Database;
using WebApi.Model;


var builder = WebApplication.CreateBuilder(args);

// Do mongoDB
builder.Services.Configure<WildBoarIotDatabaseSettings>(
    builder.Configuration.GetSection("WildBoarIotDatabase"));

builder.Services.AddSingleton<WildBoarIotDataService>();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddScoped<IWildBoarIotDataService, WildBoarIotDataService>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();