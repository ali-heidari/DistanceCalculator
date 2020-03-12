using System;
using System.Threading.Tasks;
using NServiceBus;

namespace GeoService
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "RabbitMQ.Receiver";

            #region ConfigureRabbitQueueName
            var endpointConfiguration = new EndpointConfiguration("RabbitMQ");
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.EnableCallbacks();
            endpointConfiguration.MakeInstanceUniquelyAddressable(DateTime.UtcNow.Ticks.ToString());
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host=localhost");
            #endregion

            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press any key to exit");
            Console.Read();
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
