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
    public class ConsentOptions
    {
        #region Public Fields

        public static readonly string InvalidSelectionErrorMessage = "Invalid selection";
        public static readonly string MustChooseOneErrorMessage = "You must pick at least one permission";
        public static bool EnableOfflineAccess = true;
        public static string OfflineAccessDescription = "Access to your applications and resources, even when you are offline";
        public static string OfflineAccessDisplayName = "Offline Access";

        #endregion Public Fields
    }
}