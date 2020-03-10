using System;
using System.Threading.Tasks;
using NServiceBus;

namespace GeoService
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "Samples.RabbitMQ.NativeIntegration.Receiver";

            #region ConfigureRabbitQueueName
            var endpointConfiguration = new EndpointConfiguration("Samples.RabbitMQ.NativeIntegration");
            // endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
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
