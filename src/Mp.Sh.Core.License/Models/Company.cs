/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="name"> The name of the company, which must be unique </param>
        /// <param name="description"> The description of the company, which must be unique </param>
        public Company(string name, string description)
        {
            this.Installations = new List<Installation>();
            this.CompaniesUsers = new List<CompaniesUsers>();

            this.Name = name;
            this.Description = description;
        }

        #endregion Public Constructors

        #region Protected Constructors

        /// <summary>
        /// Created for Entity Framework Proxy Don't code in this one 
        /// </summary>
        protected Company()
        {
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// The Users associated with this Company 
        /// </summary>
        public List<CompaniesUsers> CompaniesUsers { get; private set; }

        /// <summary>
        /// An additional Description for the Company 
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The Unique Id of the Company 
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// The available Installations for the Company 
        /// </summary>
        public List<Installation> Installations { get; private set; }

        /// <summary>
        /// The Unique Name of the Company 
        /// </summary>
        public string Name { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Change the current Information related to the Company 
        /// </summary>
        /// <param name="name"> The new Name of the Company </param>
        /// <param name="description"> The new Description of the Company </param>
        /// <returns> Returns the Company, modified </returns>
        public Company ChangeInformation(string name, string description)
        {
            this.Name = name;
            this.Description = description;
            return this;
        }

        /// <summary>
        /// Create a new Installation for this Company 
        /// </summary>
        /// <param name="startDate"> The starting date of the Installation </param>
        /// <param name="clientele"> The Clientele Url </param>
        /// <param name="hub"> The Service Hub Url </param>
        /// <param name="odata"> The OData Url </param>
        public Installation CreateInstallation(DateTime startDate, string clientele, string hub, string odata)
        {
            var installation = new Installation(this, startDate, clientele, hub, odata);
            this.Installations.Add(installation);
            return installation;
        }

        /// <summary>
        /// Evaluate the available Installations and return true if the Company has a valid License active 
        /// </summary>
        /// <returns> True if the Company is Licensed </returns>
        public bool IsLicensed()
        {
            // any installation before now? no, so no license available
            if (!Installations.Any(x => x.StartDate < DateTime.UtcNow))
            {
                return false;
            }

            // yes, any still valid?
            if (Installations.Any(x => x.EndDate == null || x.EndDate > DateTime.UtcNow))
            {
                return true;
            }

            return false;
        }

        #endregion Public Methods
    }
}