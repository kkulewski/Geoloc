using System;

namespace Geoloc.Models.WebModels
{
    public class RelationWebModel
    {
        public Guid Id { get; set; }

        public Guid InvitingUserId { get; set; }

        public Guid InvitedUserId { get; set; }

        public string RelationStatus { get; set; }

        public string InvitingUserName { get; set; }

        public string InvitedUserName { get; set; }
    }
}
