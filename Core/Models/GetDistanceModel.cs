namespace Core.Models
{
    public class GetDistanceModel : JWTModel
    {
        public float startingLat { get; set; }
        public float startingLng { get; set; }
        public float endingLat { get; set; }
        public float endingLng { get; set; }
    }
}