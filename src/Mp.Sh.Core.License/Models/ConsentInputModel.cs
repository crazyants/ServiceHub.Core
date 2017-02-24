/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mp.Sh.Core.License.Models
{
    [DataContract]
    public class ConsentInputModel
    {
        #region Public Properties

        [DataMember(Name = "choice")]
        public string Choice { get; set; }

        [DataMember(Name = "remember")]
        public bool Remember { get; set; }

        [DataMember(Name = "returnUrl")]
        public string ReturnUrl { get; set; }

        [DataMember(Name = "scopes")]
        public IEnumerable<string> Scopes { get; set; }

        #endregion Public Properties
    }
}