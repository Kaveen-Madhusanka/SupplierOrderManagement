using System.Text.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ;
using RabbitMQ.Client;
using SOM.ProductService.Application.SupplierInfo.Commands;
using SOM.ProductService.Domain.Supplier;
using SOM.Shared.Enums;
using SOM.Shared.SettingOptions;

namespace SOM.ProductService.BackgroundTasks;

public class ProductConsumer : ConsumerBase
{
    private static readonly List<string> BindKeys =  new()
    {
        SupplierEventEnum.SupplierCreated.ToString(),
        SupplierEventEnum.SupplierUpdated.ToString(),
        SupplierEventEnum.SupplierDeleted.ToString()
    };

   
    public ProductConsumer(IConnectionFactory connectionFactory, 
        IServiceScopeFactory serviceScopeFactory, 
        IOptions<EventBusOptions> eventBusOptions)
        : base(connectionFactory, serviceScopeFactory, eventBusOptions, BindKeys)
    {
    }

    protected override async Task ProcessMessageAsync(string message, string routingKey, CancellationToken cancellationToken)
    {
        using var scope = ServiceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        if (routingKey == SupplierEventEnum.SupplierCreated.ToString())
        {
            var supplierInfo = JsonSerializer.Deserialize<SupplierInfo>(message);
            await mediator.Send(new CreateSupplierInfoCommand
            {
                Id = supplierInfo!.Id,
                Name = supplierInfo.SupplierName
            }, cancellationToken);
        }
    }
}