/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Mp.Sh.Core.License.Models
{
    /// <summary>
    /// Override the ASP.NET Identity Role by adding a new type of PK 
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}