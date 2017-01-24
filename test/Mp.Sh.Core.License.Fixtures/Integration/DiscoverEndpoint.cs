/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using Xunit;
using FluentAssertions;
using System;
using System.Net;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;

namespace Mp.Sh.Core.License.Fixtures.Integration
{
    public class DiscoverEndpoint : IDisposable
    {
        #region Private Fields

        private readonly HttpClient client;
        private readonly TestServer server;

        #endregion Private Fields

        #region Public Constructors

        public DiscoverEndpoint()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            builder.UseUrls("http://localhost:83");
            server = new TestServer(builder);
            client = server.CreateClient();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public async void DiscoverEndpoint_Should_GrantClientCredentials_ForOData()
        {
            var content = new FormUrlEncodedContent(
                new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", "api_client"),
                    new KeyValuePair<string, string>("client_secret", "secret")
                }
            );

            var response = await client.PostAsync("/connect/token", content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void DiscoverEndpoint_Should_Returns_200()
        {
            var response = await client.GetAsync("/.well-known/openid-configuration");
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void DiscoverEndpoint_Should_Returns_ValidJson()
        {
            var response = await client.GetAsync("/.well-known/openid-configuration");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            object responseJson = JsonConvert.DeserializeObject(responseString);

            responseJson.Should().NotBeNull();
        }

        public void Dispose()
        {
            client.Dispose();
            server.Dispose();
        }

        #endregion Public Methods
    }
}