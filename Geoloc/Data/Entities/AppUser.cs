using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Geoloc.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<AppUserInMeeting> Meetings { get; set; }
    }
}
