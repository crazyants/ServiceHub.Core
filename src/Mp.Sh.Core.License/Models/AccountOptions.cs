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
    public class AccountOptions
    {
        #region Public Fields

        public static readonly string WindowsAuthenticationDisplayName = "Windows";
        public static readonly string WindowsAuthenticationProviderName = "Windows";

        // specify the Windows authentication schemes you want to use for authentication
        public static readonly string[] WindowsAuthenticationSchemes = new string[] { "Negotiate", "NTLM" };

        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = true;
        public static bool AutomaticRedirectAfterSignOut = false;
        public static string InvalidCredentialsErrorMessage = "Invalid username or password";
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt = true;
        public static bool WindowsAuthenticationEnabled = true;

        #endregion Public Fields
    }
}