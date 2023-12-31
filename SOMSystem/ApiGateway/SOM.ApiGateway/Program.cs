using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using SOM.ApiGateway.Aggregators;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration)
    .AddSingletonDefinedAggregator<SuppliersAndProductsAggregator>();

var app = builder.Build();
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");



app.Run();
