using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ShipsInSpace.Models
{
    public class User : IdentityUser<int>
    {
    }

    public class Role : IdentityRole<int>
    {
        public Role(string role) : base(role)
        {
        }

        public Role()
        {
        }
    }
}