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
        public int Id { get; set; }
        [Column("AppUsr_Username")]
        public string Username { get; set; }

        [Column("AppUsr_NormalizedUserName")]
        public string NormalizedUserName { get; set; }

        [Column("AppUsr_EmailConfirmed")]
        public int EmailConfirmed { get; set; }

        [Column("AppUsr_SecurityStamp")]
        public string SecurityStamp { get; set; }

        [Column("AppUsr_ConcurrencyStamp")]
        public string ConcurrencyStamp { get; set; }

        [Column("AppUsr_PhoneNumber")]
        public string PhoneNumber { get; set; }
        
        [Column("AppUsr_PhoneNumberConfirmed")]
        public int PhoneNumberConfirmed { get; set; }

        [Column("AppUsr_TwoFactorEnabled")]
        public int TwoFactorEnabled { get; set; }

        [Column("AppUsr_LockoutEnd")]
        public string LockoutEnd { get; set; }

        [Column("AppUsr_LockoutEnabled")]
        public int LockoutEnabled { get; set; }

        [Column("AppUsr_AccessFailedCount")]
        public int AccessFailedCount { get; set; }

        [Column("AppUsr_Email")]
        public string Email { get; set; }
        [Column("AppUsr_PasswordHash")]
        public string PasswordHash { get; set; }

        [InverseProperty("AppUsrRlAppUsr")]
        public ICollection<AppUserToRole> AppUserToRole { get; set; }
    }
}
