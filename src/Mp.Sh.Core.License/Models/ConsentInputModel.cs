/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System.Collections.Generic;

namespace Mp.Sh.Core.License.Models
{
    public class ConsentInputModel
    {
        #region Public Properties

        public string Button { get; set; }
        public bool RememberConsent { get; set; }
        public string ReturnUrl { get; set; }
        public IEnumerable<string> ScopesConsented { get; set; }

        #endregion Public Properties
    }
}