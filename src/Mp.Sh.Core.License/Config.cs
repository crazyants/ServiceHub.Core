/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using IdentityServer4.Models;
using System.Collections.Generic;

namespace Mp.Sh.Core.License
{
    /// <summary>
    /// Provide a [temporary] security configuration to get started 
    /// </summary>
    public class Config
    {
        #region Public Methods

        /// <summary>
        /// Provide a list of Api Resources that are protected 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("account", "Account APIs"),
                new ApiResource("odata", "OData APIs")
            };
        }

        /// <summary>
        /// Provide a list of Clients with the correspective APIs authorization 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "admin_client",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // scopes that client has access to
                    AllowedScopes = { "account", "odata" }
                },
                new Client
                {
                    ClientId = "api_client",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // scopes that client has access to
                    AllowedScopes = { "odata" }
                }
            };
        }

        #endregion Public Methods
    }
}