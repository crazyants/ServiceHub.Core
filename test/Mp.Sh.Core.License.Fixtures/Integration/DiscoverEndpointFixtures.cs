/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace Mp.Sh.Core.License.Fixtures.Integration
{
    [Trait("Controller", "IdentityServer")]
    public class DiscoverEndpointFixtures : IDisposable
    {
        #region Private Fields

        private readonly HttpClient client;
        private readonly ITestOutputHelper output;
        private readonly TestServer server;

        #endregion Private Fields

        #region Public Constructors

        public DiscoverEndpointFixtures(ITestOutputHelper output)
        {
            this.output = output;
            var builder = new WebHostBuilder().UseStartup<Startup>();
            builder.UseUrls("http://localhost:83");
            server = new TestServer(builder);
            client = server.CreateClient();
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            client.Dispose();
            server.Dispose();
        }

        [Fact(Skip = "It's not ready yet")]
        public async void When_Authenticate_AdminClient_Should_ReturnToken()
        {
            var content = new FormUrlEncodedContent(
                new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", "admin_client"),
                    new KeyValuePair<string, string>("client_secret", "secret")
                }
            );

            var response = await client.PostAsync("/connect/token", content);
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(Skip = "It's not ready yet")]
        public async void When_Authenticate_Alice_Should_ReturnToken()
        {
            var content = new FormUrlEncodedContent(
                new[] {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "alice"),
                    new KeyValuePair<string, string>("password", "password"),
                    new KeyValuePair<string, string>("client_id", "ro_client"),
                    new KeyValuePair<string, string>("client_secret", "secret")
                }
            );

            var response = await client.PostAsync("/connect/token", content);
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(Skip = "It's not ready yet")]
        public async void When_Authenticate_Alice_WithoutClient_Should_Return400()
        {
            var content = new FormUrlEncodedContent(
                new[] {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "alice"),
                    new KeyValuePair<string, string>("password", "password")
                }
            );

            var response = await client.PostAsync("/connect/token", content);
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(Skip = "It's not ready yet")]
        public async void When_Authenticate_ApiClient_Should_ReturnToken()
        {
            var content = new FormUrlEncodedContent(
                new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", "api_client"),
                    new KeyValuePair<string, string>("client_secret", "secret")
                }
            );

            var response = await client.PostAsync("/connect/token", content);
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(Skip = "It's not ready yet")]
        public async void When_CallDiscover_Should_Return200()
        {
            var response = await client.GetAsync("/.well-known/openid-configuration");
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(Skip = "It's not ready yet")]
        public async void When_CallDiscover_Should_ReturnJson()
        {
            var response = await client.GetAsync("/.well-known/openid-configuration");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);
            object responseJson = JsonConvert.DeserializeObject(responseString);

            responseJson.Should().NotBeNull();
        }

        #endregion Public Methods
    }
}