using UserService;
using WebApi.RabbitMQ;
using WebApi.Services;
using WebApi.Database;
using WebApi.Model;


var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Do mongoDB
builder.Services.Configure<WildBoarIotDatabaseSettings>(
    builder.Configuration.GetSection("WildBoarIotDatabase"));

builder.Services.AddSingleton<WildBoarIotDataService>();

builder.Services.AddMvc(options => options.OutputFormatters.Add(new CustomCsvFormatter()));

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("*")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

builder.Services.AddScoped<IWildBoarIotDataService, WildBoarIotDataService>();
//builder.Services.AddHostedService<RabbitMQService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<RabbitMQConsumer>();
builder.Services.AddHostedService<RabbitMQConsumer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();