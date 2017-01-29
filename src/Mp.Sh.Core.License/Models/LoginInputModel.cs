/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System.ComponentModel.DataAnnotations;

namespace Mp.Sh.Core.License.Models
{
    public class LoginInputModel
    {
        #region Public Properties

        [Required]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }

        [Required]
        public string Username { get; set; }

        #endregion Public Properties
    }
}