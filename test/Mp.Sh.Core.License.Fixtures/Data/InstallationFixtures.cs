/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using FluentAssertions;
using Mp.Sh.Core.Fixtures;
using Mp.Sh.Core.License.Data;
using Mp.Sh.Core.License.Models;
using System;
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
        public void When_SaveNew_WithoutCompany_Should_Throw()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var installation = new Installation { };
                context.Set<Installation>().Add(installation);
                Action action = () => context.SaveChanges();
                action.ShouldThrow<Exception>();
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