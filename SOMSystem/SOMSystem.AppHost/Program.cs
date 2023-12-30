using Aspire.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var rabbitMqContainer = builder.AddRabbitMQContainer("EventBus");
//var rabbitMqContainer = builder.AddRabbitMQConnection("EventBus");

builder.AddProject<SOM_SupplierServiceApi>("som.supplierserviceapi")
    .WithReference(rabbitMqContainer);

builder.AddProject<SOM_ProductServiceApi>("som.productserviceapi")
    .WithReference(rabbitMqContainer);

builder.Build().Run();
 