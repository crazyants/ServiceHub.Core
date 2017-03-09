/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mp.Sh.Core.License.Models;
using System;

namespace Mp.Sh.Core.License.Data
{
    /// <summary>
    /// Entity Framework context for ASP.NET Identity 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        #region Public Constructors

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* ASP.NET Identity Schema Customization */
            builder.Entity<ApplicationUser>(i =>
            {
                i.ToTable("Users");
                i.HasKey(x => x.Id);
            });
            builder.Entity<ApplicationRole>(i =>
            {
                i.ToTable("Roles");
                i.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserRole<Guid>>(i =>
            {
                i.ToTable("UserRoles");
                i.HasKey(x => new { x.RoleId, x.UserId });
            });
            builder.Entity<IdentityUserLogin<Guid>>(i =>
            {
                i.ToTable("UserLogins");
                i.HasKey(x => new { x.ProviderKey, x.LoginProvider });
            });
            builder.Entity<IdentityRoleClaim<Guid>>(i =>
            {
                i.ToTable("RoleClaims");
                i.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserClaim<Guid>>(i =>
            {
                i.ToTable("UserClaims");
                i.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserToken<Guid>>(i =>
            {
                i.ToTable("UserTokens");
                i.HasKey(x => x.UserId);
            });
            builder.Entity<Company>(i =>
            {
                i.ToTable("Companies");
                i.HasKey(x => x.Id);
            });
            builder.Entity<Installation>(i =>
            {
                i.ToTable("Installations");
                i.HasKey(x => x.Id);
            });

            /* Relationships Mapping */
            builder.Entity<Installation>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Installations)
                .IsRequired();
            builder.Entity<ApplicationUser>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Users)
                .IsRequired();
        }

        #endregion Protected Methods
    }
}