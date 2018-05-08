using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geoloc.Data.Entities
{
    public class Relation
    {
        public Guid Id { get; set; }
        public Guid InvitingUserId { get; set; }
        public Guid InvitedUserId { get; set; }
        public RelationStatus RelationStatus { get; set; }
            
        [ForeignKey("InvitingUserId")]
        public User InvitingUser { get; set; }
        [ForeignKey("InvitedUserId")]
        public User InvitedUser { get; set; }
    }
}
