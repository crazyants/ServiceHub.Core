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
    public class ProcessConsentResult
    {
        #region Public Properties

        public bool HasValidationError => ValidationError != null;
        public bool IsRedirect => RedirectUri != null;
        public string RedirectUri { get; set; }

        public bool ShowView => ViewModel != null;
        public string ValidationError { get; set; }
        public ConsentViewModel ViewModel { get; set; }

        #endregion Public Properties
    }
}