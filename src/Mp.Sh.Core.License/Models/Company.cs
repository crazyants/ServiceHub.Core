/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System;
using System.Collections.Generic;

namespace Mp.Sh.Core.License.Models
{
    /// <summary>
    /// It represents a Company inside the system 
    /// </summary>
    /// <remarks> The Company must have a Unique Name </remarks>
    public class Company
    {
        #region Public Constructors

        /// <summary>
        /// Used to Initialize the Company object 
        /// </summary>
        public Company()
        {
            this.Installations = new List<Installation>();
            this.CompaniesUsers = new List<CompaniesUsers>();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The Users associated with this Company 
        /// </summary>
        public List<CompaniesUsers> CompaniesUsers { get; set; }

        /// <summary>
        /// An additional Description for the Company 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Unique Id of the Company 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The available Installations for the Company 
        /// </summary>
        public List<Installation> Installations { get; set; }

        /// <summary>
        /// The Unique Name of the Company 
        /// </summary>
        public string Name { get; set; }

        #endregion Public Properties
    }
}