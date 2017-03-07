/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System;

namespace Mp.Sh.Core.License.Models
{
    /// <summary>
    /// Represents an Installation for a Company 
    /// </summary>
    public class Installation
    {
        #region Public Properties

        /// <summary>
        /// The URL where Clientele is available 
        /// </summary>
        public string Clientele { get; set; }

        /// <summary>
        /// The Owner Company 
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// The optional End Date for the installation 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// The URL where Service Hub is available 
        /// </summary>
        public string Hub { get; set; }

        /// <summary>
        /// The Unique Id of the Installation 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The URL where OData is available 
        /// </summary>
        public string Odata { get; set; }

        /// <summary>
        /// The Starting Date of this Installation 
        /// </summary>
        public DateTime StartDate { get; set; }

        #endregion Public Properties
    }
}