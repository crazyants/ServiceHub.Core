/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System;
using System.Diagnostics.Contracts;
using Mp.Sh.Core.Locales;

namespace Mp.Sh.Core.License.Models
{
    /// <summary>
    /// Represents an Installation for a Company 
    /// </summary>
    public class Installation
    {
        #region Internal Constructors

        internal Installation(Company company, DateTime startDate, string clientele, string hub, string odata)
        {
            Contract.Requires(company != null, Translations.Company_NotNull);
            Contract.Requires(!string.IsNullOrEmpty(clientele), Translations.Clientele_NotNull);
            Contract.Requires(!string.IsNullOrEmpty(hub), Translations.Hub_NotNull);
            Contract.Requires(!string.IsNullOrEmpty(odata), Translations.Odata_NotNull);
            Contract.Requires(startDate != null, Translations.StartDate_NotNull);
            this.Company = company;
            this.StartDate = startDate;
            this.Clientele = clientele;
            this.Hub = hub;
            this.Odata = odata;
        }

        #endregion Internal Constructors

        #region Protected Constructors

        /// <summary>
        /// Required by Entity Framework Proxy 
        /// </summary>
        protected Installation()
        {
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// The URL where Clientele is available 
        /// </summary>
        public string Clientele { get; private set; }

        /// <summary>
        /// The Owner Company 
        /// </summary>
        public Company Company { get; private set; }

        /// <summary>
        /// The optional End Date for the installation 
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// The URL where Service Hub is available 
        /// </summary>
        public string Hub { get; private set; }

        /// <summary>
        /// The Unique Id of the Installation 
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// The URL where OData is available 
        /// </summary>
        public string Odata { get; private set; }

        /// <summary>
        /// The Starting Date of this Installation 
        /// </summary>
        public DateTime StartDate { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Set the End Date of the Installation 
        /// </summary>
        /// <param name="endDate"> The End Date of the Installation </param>
        /// <returns> Returns the existing installation </returns>
        public Installation SetEndDate(DateTime endDate)
        {
            this.EndDate = endDate;
            return this;
        }

        #endregion Public Methods
    }
}