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
        public int Id { get; set; }
        [Column("AppUsrRl_AppUsrId")]
        public int UserId { get; set; }
        [Column("AppUsrRl_AppRoleId")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("AppUserToRole")]
        public AppRole AppUsrRlAppRole { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("AppUserToRole")]
        public AppUser AppUsrRlAppUsr { get; set; }
    }
}
