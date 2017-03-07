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

namespace Mp.Sh.Core.Fixtures
{
    /// <summary>
    /// Base fixture class used to provide common functionalities for SQL in memory testing 
    /// </summary>
    public abstract class BaseDataFixture<TContext> : IDisposable where TContext : DbContext
    {
        #region Protected Fields

        protected IDbConnection connection;
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

            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            this.options = new DbContextOptionsBuilder<TContext>()
                .UseSqlite((SqliteConnection)connection)
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
        /// Should be overriden and provides instructions to finalize and destroy a Test Database 
        /// </summary>
        protected abstract void FinalizeDatabase();

        /// <summary>
        /// Should be overriden and provides instructions to initialize a Database schema 
        /// </summary>
        protected abstract void InitializeDatabase();

        #endregion Protected Methods
    }
}