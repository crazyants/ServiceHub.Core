/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
    public class CompanyFixtures : BaseDataFixture<ApplicationDbContext>
    {
        #region Public Constructors

        public CompanyFixtures(ITestOutputHelper output) : base(output)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void When_DeleteExisting_Should_RemoveOnDatabase()
        {
            using (var context = BuildContext())
            {
                var company = ModelMockFactory.BuildCompany();
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var expected = context.Set<Company>().Single();
                context.Set<Company>().Remove(expected);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                context.Set<Company>().Count().Should().Be(0);
            }
        }

        [Fact]
        public void When_DeleteExisting_WithInstallations_Should_DeleteAlsoInstallations()
        {
            var company = ModelMockFactory.BuildCompany();
            company.CreateInstallation(DateTime.Today, "clientele", "hub", "odata");

            using (var context = BuildContext())
            {
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var expected = context.Set<Company>().Single();
                context.Set<Company>().Remove(expected);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                context.Set<Company>().Count().Should().Be(0);
                context.Set<Installation>().Count().Should().Be(0);
            }
        }

        [Fact]
        public void When_ModifyExisting_Should_ModifyOnDatabase()
        {
            using (var context = BuildContext())
            {
                var company = ModelMockFactory.BuildCompany();
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var expected = context.Set<Company>().Single()
                    .ChangeInformation(name: "new name xxx", description: "new description xxx");
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var expected = context.Set<Company>().Single();
                expected.Name.Should().Be("new name xxx");
                expected.Description.Should().Be("new description xxx");
            }
        }

        [Fact]
        public void When_SaveNew_Should_CreateId()
        {
            using (var context = BuildContext())
            {
                var company = ModelMockFactory.BuildCompany();
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var expected = context.Set<Company>().Single();
                expected.Id.Should().NotBe(Guid.Empty);
            }
        }

        [Fact]
        public void When_SaveNew_Should_ExistsOnDatabase()
        {
            using (var context = BuildContext())
            {
                var company = ModelMockFactory.BuildCompany();
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                context.Set<Company>().Count().Should().Be(1);
            }
        }

        [Fact]
        public void When_SaveNew_WithExistingName_ShouldThrow()
        {
            using (var context = BuildContext())
            {
                var company = ModelMockFactory.BuildCompany();
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var company = ModelMockFactory.BuildCompany();
                context.Set<Company>().Add(company);

                Action action = () => context.SaveChanges();
                action.ShouldThrow<Exception>();
            }
        }

        [Fact]
        public void When_SaveNew_WithInstallation_Should_SaveCompanyAndInstallation()
        {
            using (var context = BuildContext())
            {
                var company = ModelMockFactory.BuildCompany();
                company.CreateInstallation(startDate: DateTime.UtcNow, clientele: "clientele", hub: "hub", odata: "odata");
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var expected = context.Set<Company>().Include(x => x.Installations).Single();
                expected.Installations.Should().HaveCount(1);
                expected.Installations.Should().OnlyContain(x => x.Company != null);
            }
        }

        [Fact]
        public void When_UpdateExisting_WithExistingName_ShouldThrow()
        {
            using (var context = BuildContext())
            {
                var company = ModelMockFactory.BuildCompany();
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var company = ModelMockFactory.BuildCompany()
                    .ChangeInformation("new name", "new description");
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var expected = context.Set<Company>()
                    .Single(x => x.Name == "name")
                    .ChangeInformation("new name", "new description");

                Action action = () => context.SaveChanges();
                action.ShouldThrow<Exception>();
            }
        }

        #endregion Public Methods
    }
}