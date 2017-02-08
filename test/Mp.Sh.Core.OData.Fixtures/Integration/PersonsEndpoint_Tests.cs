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
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mp.Sh.Core.OData.Fixtures.Integration
{
    public class PersonsEndpoint_Tests : IDisposable
    {
        #region Private Fields

        private readonly HttpClient apiClient;
        private readonly TestServer apiServer;
        private readonly IWebHost intServer;
        private readonly ITestOutputHelper output;

        #endregion Private Fields

        #region Public Constructors

        public PersonsEndpoint_Tests(ITestOutputHelper output)
        {
            this.output = output;
            var apiBuilder = new WebHostBuilder()
                .UseStartup<OData.Startup>();
            apiBuilder.UseUrls("http://localhost:82");
            apiServer = new TestServer(apiBuilder);
            apiClient = apiServer.CreateClient();

            intServer = new WebHostBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseStartup<License.Startup>()
                .UseUrls("http://locahost:83").Build();
            intServer.Start();
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            apiClient.Dispose();
            apiServer.Dispose();
            intServer.Dispose();
        }

        [Fact]
        [Trait("Category", "Person")]
        public async void PersonsEndpoint_Authenticated_WithClientCode_Returns_200()
        {
            var token = GetClientToken();
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Result}");

            var response = await apiClient.GetAsync("/persons");
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "Person")]
        public async void PersonsEndpoint_Authenticated_WithUser_Returns_200()
        {
            var token = GetUserToken();
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Result}");

            var response = await apiClient.GetAsync("/persons");
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "Person")]
        public async void PersonsEndpoint_NotAuthenticated_Returns_401()
        {
            var response = await apiClient.GetAsync("/persons");
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<string> GetClientToken()
        {
            HttpClient idenClient = new HttpClient();
            idenClient.BaseAddress = new Uri("http://localhost:83");

            var content = new FormUrlEncodedContent(
                new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", "api_client"),
                    new KeyValuePair<string, string>("client_secret", "secret")
                }
            );

            var response = await idenClient.PostAsync("/connect/token", content);
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);
            dynamic responseJson = JsonConvert.DeserializeObject(responseString);
            return responseJson.access_token;
        }

        private async Task<string> GetUserToken()
        {
            HttpClient idenClient = new HttpClient();
            idenClient.BaseAddress = new Uri("http://localhost:83");

            var content = new FormUrlEncodedContent(
                new[] {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "alice"),
                    new KeyValuePair<string, string>("password", "password"),
                    new KeyValuePair<string, string>("client_id", "ro_client"),
                    new KeyValuePair<string, string>("client_secret", "secret")
                }
            );

            var response = await idenClient.PostAsync("/connect/token", content);
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);
            dynamic responseJson = JsonConvert.DeserializeObject(responseString);
            return responseJson.access_token;
        }

        #endregion Private Methods
    }
}