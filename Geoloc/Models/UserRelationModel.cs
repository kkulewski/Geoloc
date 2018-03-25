using System;
using Geoloc.Data.Entities;

namespace Geoloc.Models
{
    public class UserRelationModel
    {
        public Guid Id { get; set; }
        public UserModel InvitingUser { get; set; }
        public UserModel InvitedUser { get; set; }
        public UserRelationStatus UserRelationStatus { get; set; }
    }
}
