using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geoloc.Data.Entities
{
    public class UserRelation
    {
        public Guid Id { get; set; }
        public Guid InvitingUserId { get; set; }
        public Guid InvitedUserId { get; set; }
        public UserRelationStatus UserRelationStatus { get; set; }
            
        [ForeignKey("InvitingUserId")]
        public AppUser InvitingUser { get; set; }
        [ForeignKey("InvitedUserId")]
        public AppUser InvitedUser { get; set; }
    }
}