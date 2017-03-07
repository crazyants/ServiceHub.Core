/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using Microsoft.Extensions.Logging;

namespace Mp.Sh.Core.Fixtures
{
    /// <summary>
    /// Base fixture class used to provide common functionalities for SQL in memory testing 
    /// </summary>
    public abstract class BaseDataFixture<TContext> : IDisposable where TContext : DbContext
    {
        #region Protected Fields

        protected IDbConnection connection;
        protected ILoggerFactory loggerFactory;
        protected DbContextOptions<TContext> options;
        protected ITestOutputHelper output;

        #endregion Protected Fields

        #region Public Constructors

        /// <summary>
        /// Initialize the test class 
        /// </summary>
        public BaseDataFixture(ITestOutputHelper output)
        {
            this.output = output;

            this.loggerFactory = new LoggerFactory();
            this.loggerFactory.AddProvider(new LoggerProviderForFixtures(output));

            this.connection = new SqliteConnection("DataSource=:memory:");
            this.connection.Open();

            this.options = new DbContextOptionsBuilder<TContext>()
                .UseSqlite((SqliteConnection)connection)
                .UseLoggerFactory(loggerFactory)
                .Options;

            this.InitializeDatabase();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Dispose resources after each test run 
        /// </summary>
        public void Dispose()
        {
            this.FinalizeDatabase();

            this.connection.Close();
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Build a new TContext using the in memory options 
        /// </summary>
        /// <returns></returns>
        protected TContext BuildContext()
        {
            var context = Activator.CreateInstance(typeof(TContext), new object[] { options }) as TContext;
            return context;
        }

        /// <summary>
        /// Should be overriden and provides instructions to finalize and destroy a Test Database 
        /// </summary>
        protected void FinalizeDatabase()
        {
            using (var context = BuildContext())
            {
                context.Database.EnsureDeleted();
            }
        }

        /// <summary>
        /// Should be overriden and provides instructions to initialize a Database schema 
        /// </summary>
        protected void InitializeDatabase()
        {
            using (var context = BuildContext())
            {
                context.Database.Migrate();

                // RG cannot use EnsureCreated because it will not take into account DB Schema migrations!!
            }
        }

        #endregion Protected Methods
    }
}