namespace Geoloc.Models.Entities
{
    public class UsersInMeeting
    {
        public int Id { get; set; }

        public AppUser AppUser { get; set; }
        public Meeting Meeting { get; set; }
    }
}