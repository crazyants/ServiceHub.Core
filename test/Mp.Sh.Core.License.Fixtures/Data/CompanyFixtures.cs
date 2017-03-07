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
            using (var context = new ApplicationDbContext(options))
            {
                var company = new Company { Id = Guid.NewGuid(), Name = "Some Name", Description = "Some Description" };
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var expected = context.Set<Company>().Single();
                context.Set<Company>().Remove(expected);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.Set<Company>().Count().Should().Be(0);
            }
        }

        [Fact]
        public void When_ModifyExisting_Should_ModifyOnDatabase()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var company = new Company { Id = Guid.NewGuid(), Name = "Some Name", Description = "Some Description" };
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var expected = context.Set<Company>().Single();
                expected.Name = "new name";
                expected.Description = "new description";
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var expected = context.Set<Company>().Single();
                expected.Name.Should().Be("new name");
                expected.Description.Should().Be("new description");
            }
        }

        [Fact]
        public void When_SaveNew_Should_CreateId()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var company = new Company { Name = "Some Name", Description = "Some Description" };
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var expected = context.Set<Company>().Single();
                expected.Id.Should().NotBe(Guid.Empty);
            }
        }

        [Fact]
        public void When_SaveNew_Should_ExistsOnDatabase()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var company = new Company { Id = Guid.NewGuid(), Name = "Some Name", Description = "Some Description" };
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.Set<Company>().Count().Should().Be(1);
            }
        }

        [Fact]
        public void When_SaveNew_WithInstallation_Should_SaveCompanyAndInstallation()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var company = new Company { Id = Guid.NewGuid(), Name = "Some Name", Description = "Some Description" };
                company.Installations.Add(new Installation { });
                context.Set<Company>().Add(company);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var expected = context.Set<Company>().Include(x => x.Installations).Single();
                expected.Installations.Should().HaveCount(1);
                expected.Installations.Should().OnlyContain(x => x.Company != null);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void FinalizeDatabase()
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
            }
        }

        protected override void InitializeDatabase()
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
            }
        }

        #endregion Protected Methods
    }
}