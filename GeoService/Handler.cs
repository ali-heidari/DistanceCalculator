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
        Core.Data.DataProvider db = Core.Data.DataProvider.DataProviderFactory();
        db.Insert(new Core.Data.GeoData()
        {
            Distance = distance,
            StartingLat = message.StartingLat,
            StartingLng = message.StartingLng,
            EndingLat = message.EndingLat,
            EndingLng = message.EndingLng,
            UserGUID = message.UserGUID
        });
        return Task.CompletedTask;
    }
}