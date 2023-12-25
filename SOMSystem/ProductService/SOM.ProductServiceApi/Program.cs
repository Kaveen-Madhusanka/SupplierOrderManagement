using RabbitMQ;
using Serilog;
using SOM.ProductService.Application;
using SOM.ProductService.Application.Mappers;
using SOM.ProductService.BackgroundTasks;
using SOM.ProductService.Infrastructure;
using SOM.Shared.Middlewares;
using SOM.Shared.SettingOptions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

builder.Services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBus"));


builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(ProductMapperProfile));
builder.Services.AddHostedService<ProductConsumer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5010;
});

builder.Services.AddRabbitMq(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
