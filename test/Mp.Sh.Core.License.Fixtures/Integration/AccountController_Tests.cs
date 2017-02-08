/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mp.Sh.Core.License.Fixtures.Integration
{
    public class AccountController_Tests : IDisposable
    {
        #region Private Fields

        private readonly HttpClient client;
        private readonly IWebHost intServer;
        private readonly ITestOutputHelper output;

        #endregion Private Fields

        #region Public Constructors

        public AccountController_Tests(ITestOutputHelper output)
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

        [Fact]
        [Trait("Category", "License Server")]
        [Trait("Category", "Account")]
        public async void AccountController_GetLogin_Return_200()
        {
            var response = await client.GetAsync("/account/login");
            var responseString = await response.Content.ReadAsStringAsync();
            output.WriteLine(responseString);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "License Server")]
        [Trait("Category", "Account")]
        public async void AccountController_UserToken_Return_200()
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

        public void Dispose()
        {
            client.Dispose();
            intServer.Dispose();
        }

        #endregion Public Methods
    }
}