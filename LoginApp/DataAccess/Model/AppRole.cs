using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public partial class AppRole
    {
        public AppRole()
        {
            AppUserToRole = new HashSet<AppUserToRole>();
        }

        [Key]
        [Column("AppRl_Id")]
        public long AppRlId { get; set; }
        [Column("AppRl_Name")]
        public string AppRlName { get; set; }

        [InverseProperty("AppUsrRlAppRole")]
        public ICollection<AppUserToRole> AppUserToRole { get; set; }
    }
}
