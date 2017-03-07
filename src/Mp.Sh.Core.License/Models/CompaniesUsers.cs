/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System;

namespace Mp.Sh.Core.License.Models
{
    public class CompaniesUsers
    {
        #region Public Properties

        public Company Company { get; set; }

        public Guid CompanyId { get; set; }

        public ApplicationUser User { get; set; }

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}