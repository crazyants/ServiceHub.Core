/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

namespace Mp.Sh.Core.AspNet.Configurations
{
    /// <summary>
    /// Provides a list of Social Providers available 
    /// </summary>
    public class SocialProviders
    {
        #region Public Properties

        /// <summary>
        /// A Collection of available Providers 
        /// </summary>
        public SocialProvider[] Providers { get; set; }

        /// <summary>
        /// The current version of the configuration section 
        /// </summary>
        public string Version { get; set; }

        #endregion Public Properties
    }
}