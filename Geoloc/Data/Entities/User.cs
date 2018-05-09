using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Geoloc.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<UserInMeeting> Meetings { get; set; }
    }
}
