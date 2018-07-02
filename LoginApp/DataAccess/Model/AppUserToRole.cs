using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public partial class AppUserToRole
    {
        [Key]
        [Column("AppUsrRl_Id")]
        public long AppUsrRlId { get; set; }
        [Column("AppUsrRl_AppUsrId")]
        public long AppUsrRlAppUsrId { get; set; }
        [Column("AppUsrRl_AppRoleId")]
        public long AppUsrRlAppRoleId { get; set; }

        [ForeignKey("AppUsrRlAppRoleId")]
        [InverseProperty("AppUserToRole")]
        public AppRole AppUsrRlAppRole { get; set; }
        [ForeignKey("AppUsrRlAppUsrId")]
        [InverseProperty("AppUserToRole")]
        public AppUser AppUsrRlAppUsr { get; set; }
    }
}
