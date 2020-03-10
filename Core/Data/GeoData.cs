
namespace Core.Data
{
    public class GeoData : DataObject
    {
        public override string DocumentName { get => "GeoData"; }
        public float StartingLat { get; set; }
        public float StartingLng { get; set; }
        public float EndingLat { get; set; }
        public float EndingLng { get; set; }
        public double Distance { get; set; }
        public string UserGUID { get; set; }
    }
}