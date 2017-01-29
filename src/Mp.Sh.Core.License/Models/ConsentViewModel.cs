/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System.Collections.Generic;

namespace Mp.Sh.Core.License.Models
{
    public class ConsentViewModel : ConsentInputModel
    {
        #region Public Properties

        public bool AllowRememberConsent { get; set; }
        public string ClientLogoUrl { get; set; }
        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }
        public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }

        #endregion Public Properties
    }
}