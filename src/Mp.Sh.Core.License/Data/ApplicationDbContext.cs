/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mp.Sh.Core.License.Models;

namespace Mp.Sh.Core.License.Data
{
    /// <summary>
    /// Entity Framework context for ASP.NET Identity 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
            // Customize the ASP.NET Identity model and override the defaults if needed. For example,
            // you can rename the ASP.NET Identity table names and more. Add your customizations
            // after calling base.OnModelCreating(builder);
        }

        #endregion Protected Methods
    }
}