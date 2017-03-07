/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using FluentAssertions;
using Mp.Sh.Core.Fixtures;
using Mp.Sh.Core.License.Data;
using Mp.Sh.Core.License.Fixtures.Mocks;
using Mp.Sh.Core.License.Models;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Mp.Sh.Core.License.Fixtures.Data
{
    [Trait("Category", "DAL")]
    public class InstallationFixtures : BaseDataFixture<ApplicationDbContext>
    {
        #region Public Constructors

        public InstallationFixtures(ITestOutputHelper output) : base(output)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void When_SaveNew_IfEndDateIsProvided_ShouldSaveEndDate()
        {
            var company = ModelMockFactory.BuildCompany();
            var installation = company
                .CreateInstallation(DateTime.UtcNow, "clientele", "hub", "odata")
                .SetEndDate(DateTime.UtcNow.AddYears(1));

            using (var context = BuildContext())
            {
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var expected = context.Set<Installation>().Single();
                expected.EndDate.Value.Should()
                    .BeAfter(DateTime.UtcNow);
            }
        }

        #endregion Public Methods
    }
}