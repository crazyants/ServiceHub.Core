/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

namespace Mp.Sh.Core.AspNet.Configurations
{
    /// <summary>
    /// Identify a single Social Provider 
    /// </summary>
    public class SocialProvider
    {
        #region Public Properties

        /// <summary>
        /// The Unique Id of the Client provided by the social provider 
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The Unique Key of the Client provided by the social provider 
        /// </summary>
        public string ClientKey { get; set; }

        /// <summary>
        /// The Name of the Social Provider 
        /// </summary>
        public string Name { get; set; }

        #endregion Public Properties
    }
}