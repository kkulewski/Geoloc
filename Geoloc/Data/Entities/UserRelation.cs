using System;
using Geoloc.Models;

namespace Geoloc.Data.Entities
{
    public class UserRelation
    {
        public Guid Id { get; set; }
        public Guid InvitingUserId { get; set; }
        public Guid InvitedUserId { get; set; }
        public UserRelationStatus UserRelationStatus { get; set; }
            
        public AppUser InvitingUser { get; set; }
        public AppUser InvitedUser { get; set; }
    }
}