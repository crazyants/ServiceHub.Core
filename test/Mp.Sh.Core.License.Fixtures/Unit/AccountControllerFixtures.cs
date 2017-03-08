/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using FluentAssertions;
using IdentityServer4.Quickstart.UI;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Mp.Sh.Core.License.Models;
using Mp.Sh.Core.License.Services;
using System;
using System.ComponentModel;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Mp.Sh.Core.License.Fixtures.Unit
{
    [Trait("Controller", "AccountController")]
    public class AccountControllerFixtures : IDisposable
    {
        #region Private Fields

        private readonly ITestOutputHelper output;
        private IdentityServer4.Quickstart.UI.AccountController accountController;
        private Mock<IClientStore> mockClientStore;
        private Mock<IHttpContextAccessor> mockHttpContextAccessor;
        private Mock<IIdentityServerInteractionService> mockInteraction;
        private TestUserStore userStore;

        #endregion Private Fields

        #region Public Constructors

        public AccountControllerFixtures(ITestOutputHelper output)
        {
            this.output = output;
            mockInteraction = new Mock<IIdentityServerInteractionService>();
            mockClientStore = new Mock<IClientStore>();
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            userStore = new TestUserStore(ConfigurationMockService.GetUsers());
            accountController = null;

            //new AccountController(
            //    mockInteraction.Object,
            //    mockClientStore.Object,
            //    mockHttpContextAccessor.Object,
            //    userStore);
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
        }

        [Fact(Skip = "It's not ready yet")]
        public async void When_LoginGet_Should_ReturnLoginViewModel()
        {
            IActionResult result = await accountController.Login("http://returnurl");
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            LoginViewModel viewModel = (LoginViewModel)viewResult.Model;
            viewModel.ExternalProviders.ToList()
                .Should().HaveCount(4);
        }

        #endregion Public Methods
    }
}