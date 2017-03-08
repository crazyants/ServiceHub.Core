/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using FluentAssertions;
using Mp.Sh.Core.License.Fixtures.Mocks;
using Mp.Sh.Core.License.Models;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Mp.Sh.Core.License.Fixtures.Unit
{
    [Trait("Model", "Company")]
    public class CompanyFixtures : IDisposable
    {
        #region Private Fields

        private Company mockCompany;
        private ITestOutputHelper output;

        #endregion Private Fields

        #region Public Constructors

        public CompanyFixtures(ITestOutputHelper output)
        {
            this.output = output;
            mockCompany = ModelMockFactory.BuildCompany();
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
        }

        [Fact]
        public void When_HasManyInstallations_WithEndDate_IsLicensed_IfOneIsNotExpired()
        {
            mockCompany
                .CreateInstallation(DateTime.UtcNow.AddDays(-1), "clientele", "hub", "odata")
                .SetEndDate(DateTime.UtcNow.AddDays(1));
            mockCompany
                .CreateInstallation(DateTime.UtcNow.AddDays(-10), "clientele", "hub", "odata")
                .SetEndDate(DateTime.UtcNow.AddDays(-10));

            mockCompany.IsLicensed().Should().BeTrue();
        }

        [Fact]
        public void When_HasManyInstallations_WithEndDate_IsNotLicensed_IfAllExpired()
        {
            mockCompany
                .CreateInstallation(DateTime.UtcNow.AddDays(-1), "clientele", "hub", "odata")
                .SetEndDate(DateTime.UtcNow.AddDays(-1));
            mockCompany
                .CreateInstallation(DateTime.UtcNow.AddDays(-10), "clientele", "hub", "odata")
                .SetEndDate(DateTime.UtcNow.AddDays(-10));

            mockCompany.IsLicensed().Should().BeFalse();
        }

        [Fact]
        public void When_HasOneInstallation_IsNotLicensed_BeforeStartDate()
        {
            var installation = mockCompany.CreateInstallation(DateTime.UtcNow.AddDays(1), "clientele", "hub", "odata");

            mockCompany.IsLicensed().Should().BeFalse();
        }

        [Fact]
        public void When_HasOneInstallation_WithEndDate_IsLicensed_BeforeExpires()
        {
            var installation = mockCompany
                .CreateInstallation(DateTime.UtcNow.AddDays(-1), "clientele", "hub", "odata")
                .SetEndDate(DateTime.UtcNow.AddDays(1));

            mockCompany.IsLicensed().Should().BeTrue();
        }

        [Fact]
        public void When_HasOneInstallation_WithEndDate_IsNotLicensed_AfterExpires()
        {
            var installation = mockCompany
                .CreateInstallation(DateTime.UtcNow.AddYears(-1), "clientele", "hub", "odata")
                .SetEndDate(DateTime.UtcNow.AddYears(-1));

            mockCompany.IsLicensed().Should().BeFalse();
        }

        [Fact]
        public void When_HasOneInstallation_WithoutEndDate_IsLicensed()
        {
            mockCompany.CreateInstallation(DateTime.UtcNow.AddDays(-1), "clientele", "hub", "odata");

            mockCompany.IsLicensed().Should().BeTrue();
        }

        #endregion Public Methods
    }
}