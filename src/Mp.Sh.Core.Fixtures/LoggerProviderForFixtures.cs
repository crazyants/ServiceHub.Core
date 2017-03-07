/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;

namespace Mp.Sh.Core.Fixtures
{
    public class LoggerForFixtures : ILogger, IDisposable
    {
        #region Private Fields

        private string categoryName;
        private ITestOutputHelper output;

        #endregion Private Fields

        #region Public Constructors

        public LoggerForFixtures(string categoryName, ITestOutputHelper output)
        {
            this.categoryName = categoryName;
            this.output = output;
        }

        #endregion Public Constructors

        #region Public Methods

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose()
        {
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            message = $"{ logLevel }: {message}";

            if (exception != null)
            {
                message += Environment.NewLine + Environment.NewLine + exception.ToString();
            }

            output.WriteLine(message);
        }

        #endregion Public Methods
    }

    public class LoggerProviderForFixtures : ILoggerProvider
    {
        #region Private Fields

        private ITestOutputHelper output;

        #endregion Private Fields

        #region Public Constructors

        public LoggerProviderForFixtures(ITestOutputHelper output)
        {
            this.output = output;
        }

        #endregion Public Constructors

        #region Public Methods

        public ILogger CreateLogger(string categoryName)
        {
            return new LoggerForFixtures(categoryName, output);
        }

        public void Dispose()
        {
        }

        #endregion Public Methods
    }
}