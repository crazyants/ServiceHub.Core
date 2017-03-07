﻿/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Mp.Sh.Core.License.Models
{
    /// <summary>
    /// Override the ASP.NET Identity User by adding a new type of PK 
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        #region Public Properties

        /// <summary>
        /// The Companies associated with this User 
        /// </summary>
        public List<CompaniesUsers> CompaniesUsers { get; set; }

        #endregion Public Properties
    }
}