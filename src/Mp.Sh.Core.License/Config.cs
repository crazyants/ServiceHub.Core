/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using IdentityServer4.Models;
using IdentityServer4.Test;
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
                },
                new Client
                {
                    ClientId = "ro_client",
                    // allow interactive user
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // this client can use only odata
                    AllowedScopes = { "odata" }
                }
            };
        }

        /// <summary>
        /// Provide a list of Users with the correspective claims 
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };
        }

        #endregion Public Methods
    }
}