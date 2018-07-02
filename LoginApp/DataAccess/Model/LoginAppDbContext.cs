using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Model
{
    public partial class LoginAppDbContext : DbContext
    {
        public LoginAppDbContext()
        {
        }

        public LoginAppDbContext(DbContextOptions<LoginAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppRole> AppRole { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<AppUserToRole> AppUserToRole { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlite("Datasource=C:\\_Dev_\\_Github_Repos_\\NetApps\\LoginApp\\DBFiles\\loginapp");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>(entity =>
            {
                entity.Property(e => e.AppRlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.AppUsrId).ValueGeneratedNever();
            });

            modelBuilder.Entity<AppUserToRole>(entity =>
            {
                entity.HasIndex(e => new { e.AppUsrRlAppUsrId, e.AppUsrRlAppRoleId })
                    .IsUnique();

                entity.Property(e => e.AppUsrRlId).ValueGeneratedNever();

                entity.HasOne(d => d.AppUsrRlAppRole)
                    .WithMany(p => p.AppUserToRole)
                    .HasForeignKey(d => d.AppUsrRlAppRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.AppUsrRlAppUsr)
                    .WithMany(p => p.AppUserToRole)
                    .HasForeignKey(d => d.AppUsrRlAppUsrId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
