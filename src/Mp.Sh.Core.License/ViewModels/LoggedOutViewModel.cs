/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mp.Sh.Core.License.Models
{
    public class LoggedOutViewModel
    {
        #region Public Properties

        public bool AutomaticRedirectAfterSignOut { get; set; }
        public string ClientName { get; set; }
        public string ExternalAuthenticationScheme { get; set; }
        public string LogoutId { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public string SignOutIframeUrl { get; set; }
        public bool TriggerExternalSignout => ExternalAuthenticationScheme != null;

        #endregion Public Properties
    }
}