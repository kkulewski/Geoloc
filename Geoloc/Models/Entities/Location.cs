namespace Geoloc.Models.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public AppUser AppUser { get; set; }
    }
}