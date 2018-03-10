using Geoloc.Data.Entities;

namespace Geoloc.Models.WebModels
{
    public class UserRelationWebModel
    {
        public string InvitingUserName { get; set; }
        public string InvitedUserName { get; set; }
        public UserRelationStatus Status { get; set; }
    }
}
