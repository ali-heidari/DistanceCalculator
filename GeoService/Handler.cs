using System.Device.Location;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using RabbitMQ.Client.Events;

public class Handler :
    IHandleMessages<GeoPoints>
{
    static ILog log = LogManager.GetLogger<Handler>();

    public Task Handle(GeoPoints message, IMessageHandlerContext context)
    {
        var sCoord = new GeoCoordinate(message.StartingLat, message.StartingLng);
        var eCoord = new GeoCoordinate(message.EndingLat, message.EndingLng);

        var distance = sCoord.GetDistanceTo(eCoord);

        log.Info($"Distance = {distance}");
        return Task.CompletedTask;
    }
}