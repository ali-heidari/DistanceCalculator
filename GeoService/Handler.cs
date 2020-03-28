using System.Device.Location;
using System.Threading.Tasks;
using Core.NServiceBus;
using NServiceBus;
using NServiceBus.Logging;
using Core.Models;

/// <summary>
/// Handles request come for calculation of distance
/// </summary>
public class Handler :
    IHandleMessages<GeoPoints>
{
    static ILog log = LogManager.GetLogger<Handler>();

    public Task Handle(GeoPoints message, IMessageHandlerContext context)
    {
        var sCoord = new GeoCoordinate(message.StartingLat, message.StartingLng);
        var eCoord = new GeoCoordinate(message.EndingLat, message.EndingLng);
        // Calc distance
        message.Distance = sCoord.GetDistanceTo(eCoord);        
        
        return context.Reply(message);
    }

}