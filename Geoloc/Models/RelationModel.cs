using System;
using Geoloc.Data.Entities;

namespace Geoloc.Models
{
    public class RelationModel
    {
        public Guid Id { get; set; }
        public UserModel InvitingUser { get; set; }
        public UserModel InvitedUser { get; set; }
        public RelationStatus RelationStatus { get; set; }
    }
}
