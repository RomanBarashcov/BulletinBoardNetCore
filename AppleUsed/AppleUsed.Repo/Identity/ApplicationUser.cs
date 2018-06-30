using AppleUsed.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Ad> Ads { get; set; }
        public ApplicationUser()
        {
            Ads = new List<Ad>();
        }
    }
}
