using System;
using Microsoft.AspNetCore.Identity;

namespace Geoloc.Data.Entities
{
    public class UserRole : IdentityRole<Guid>
    {
        public UserRole(string roleName): base (roleName)
        {
        }
    }
}
