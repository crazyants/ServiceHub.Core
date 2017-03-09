/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace Mp.Sh.Core.License.Fixtures.Integration
{
    [Trait("Controller", "AccountController")]
    public class AccountControllerFixtures : IDisposable
    {
        #region Private Fields

        private readonly HttpClient client;
        private readonly IWebHost intServer;
        private readonly ITestOutputHelper output;

        #endregion Private Fields

        #region Public Constructors

        public AccountControllerFixtures(ITestOutputHelper output)
        {
            this.output = output;

            intServer = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(@"..\..\..\..\..\src\Mp.Sh.Core.License")
                .UseIISIntegration()
                .UseStartup<License.Startup>()
                .UseUrls("http://locahost:83").Build();
            intServer.Start();

            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:83");
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            client.Dispose();
            intServer.Dispose();
        }

        [Fact(Skip = "It's not ready yet")]
        public async void When_LoginGet_Should_Return200()
        {
            var response = await client.GetAsync("/account/login");
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(Skip = "It's not ready yet")]
        public async void When_Token_WithValidCredentials_Should_Return200()
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

        #endregion Public Methods
    }
}