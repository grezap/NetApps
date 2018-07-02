using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int AppUsrId { get; set; }
        public string AppUsrUsername { get; set; }
        public string AppUsrEmail { get; set; }
        public string AppUsrPasswordHash { get; set; }
    }
}
