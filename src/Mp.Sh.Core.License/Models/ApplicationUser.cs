/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Mp.Sh.Core.Locales;
using Mp.Sh.Core.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Mp.Sh.Core.License.Models
{
    /// <summary>
    /// Override the ASP.NET Identity User by adding a new type of PK 
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {

        /// <summary>
        /// Required by Entity Framework
        /// </summary>
        protected ApplicationUser()
        {

        }

        /// <summary>
        /// Create a new Application User by providing an Email and a Password
        /// </summary>
        /// <param name="email">The Email address, which is also the Username</param>
        /// <param name="passwordHash">The Password hash</param>
        public ApplicationUser(Company company, string email, string passwordHash)
        {
            Contract.Requires(!string.IsNullOrEmpty(email), Translations.Email_NotNull);
            Contract.Requires(!string.IsNullOrEmpty(passwordHash), Translations.Password_NotNull);
            Contract.Requires(email.IsEmail(), Translations.Email_Invalid);
            this.Email = email;
            this.UserName = email;
            this.PasswordHash = passwordHash;
        }

        /// <summary>
        /// The Owner's Company for this User
        /// </summary>
        public Company Company { get; private set; }

        #region Public Properties

        #endregion Public Properties
    }
}