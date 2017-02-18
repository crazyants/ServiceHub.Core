﻿/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace Mp.Sh.Core.License.Services
{
    /// <summary>
    /// Provide a [temporary] security configuration to get started 
    /// </summary>
    public class ConfigurationMockService
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
                    // where to redirect to after login
                    RedirectUris = {
                        "http://localhost:81/signin-oidc",
                        "https://www.getpostman.com/oauth2/callback" },
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
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "odata"  // backend api calls
                    },
                },
                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc_client",
                    ClientName = "MVC Client",
                    LogoUri = "http://localhost:81/img/sh-rgb-57.png",
                    // allow access token + API Bearer authorization calls
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    // Client secrets for access token
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // where to redirect to after login
                    RedirectUris = {
                        "http://localhost:81/signin-oidc",
                        "https://www.getpostman.com/oauth2/callback" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:81" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "odata"  // backend api calls
                    },
                    AlwaysIncludeUserClaimsInIdToken = true, // this will return all claims
                    // refresh token
                    AllowOfflineAccess = true
                }
            };
        }

        /// <summary>
        /// Provide a list of supported OIDC (OpenID Connect) info (i.e. Subject ID, Profile, Username) 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone()
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