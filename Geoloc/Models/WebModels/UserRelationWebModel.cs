using System;
using Geoloc.Data.Entities;

namespace Geoloc.Models.WebModels
{
    public class UserRelationWebModel
    {
        public Guid Id { get; set; }

        public Guid InvitingUserId { get; set; }

        public Guid InvitedUserId { get; set; }

        public string UserRelationStatus { get; set; }

        public string InvitingUserName { get; set; }

        public string InvitedUserName { get; set; }
    }
}
