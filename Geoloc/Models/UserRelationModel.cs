using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geoloc.Data.Entities;

namespace Geoloc.Models
{
    public class UserRelationModel
    {
        public Guid Id { get; set; }
        public Guid InvitingUserId { get; set; }
        public Guid InvitedUserId { get; set; }
        public UserRelationStatus UserRelationStatus { get; set; }
    }
}
