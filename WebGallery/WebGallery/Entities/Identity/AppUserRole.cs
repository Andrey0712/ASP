using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGallery.Entities.Identity
{
    public class AppUserRole : IdentityUserRole<long>
    {
        public virtual AppRole Role { get; set; }
        public virtual AppUser User { get; set; }

}
}
