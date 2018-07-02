using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public partial class AppUser
    {
        public AppUser()
        {
            AppUserToRole = new HashSet<AppUserToRole>();
        }

        [Key]
        [Column("AppUsr_Id")]
        public long AppUsrId { get; set; }
        [Column("AppUsr_Username")]
        public string AppUsrUsername { get; set; }
        [Column("AppUsr_Email")]
        public string AppUsrEmail { get; set; }
        [Column("AppUsr_PasswordHash")]
        public string AppUsrPasswordHash { get; set; }

        [InverseProperty("AppUsrRlAppUsr")]
        public ICollection<AppUserToRole> AppUserToRole { get; set; }
    }
}
