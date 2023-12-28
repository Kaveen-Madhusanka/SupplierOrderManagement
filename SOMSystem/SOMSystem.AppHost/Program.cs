var builder = DistributedApplication.CreateBuilder(args);

var rabbitMqContainer = builder.AddRabbitMQContainer("EventBus");

builder.AddProject<Projects.SOM_SupplierServiceApi>("som.supplierserviceapi")
    .WithReference(rabbitMqContainer);

builder.AddProject<Projects.SOM_ProductServiceApi>("som.productserviceapi")
    .WithReference(rabbitMqContainer);



builder.Build().Run();
 