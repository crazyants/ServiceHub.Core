/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Mp.Sh.Core.AspNet.Configurations;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Mp.Sh.Core.AspNet.Fixtures.Unit
{
    [Trait("Utilities", "Configuration")]
    public class SocialProvidersConfigurationFixtures : IDisposable
    {
        #region Private Fields

        private readonly IConfigurationRoot configuration;
        private readonly ITestOutputHelper output;
        private readonly SocialProviders providers;
        private readonly IConfigurationSection section;

        #endregion Private Fields

        #region Public Constructors

        public SocialProvidersConfigurationFixtures(ITestOutputHelper output)
        {
            this.output = output;

            var builder = new ConfigurationBuilder()
                .SetBasePath(new FileInfo(typeof(SocialProvidersConfigurationFixtures).GetTypeInfo().Assembly.Location).Directory.FullName)
                .AddJsonFile("mock.providers.json");
            configuration = builder.Build();

            configuration.Should().NotBeNull("Cannot initialize the tests, the configuration is not loaded");

            section = configuration.GetSection("SocialProviders");
            providers = section.Get<SocialProviders>();
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
        }

        [Fact]
        public void When_FacebookConfigured_Should_ReturnProvider()
        {
            providers.Providers.Should()
                .ContainSingle(p => p.Name == "Facebook");
        }

        [Fact]
        public void When_GoogleConfigured_Should_ReturnProvider()
        {
            providers.Providers.Should()
                .ContainSingle(p => p.Name == "Google");
        }

        [Fact]
        public void When_LinkedInConfigured_Should_ReturnProvider()
        {
            providers.Providers.Should()
                .ContainSingle(p => p.Name == "LinkedIn");
        }

        [Fact]
        public void When_ProviderConfigured_Should_ContainClientId()
        {
            providers.Providers.First()
                .ClientId.Should().Be("12345-ABCDE");
        }

        [Fact]
        public void When_ProviderConfigured_Should_ContainClientKey()
        {
            providers.Providers.First()
                .ClientKey.Should().Be("99999-XXXXX");
        }

        [Fact]
        public void When_ProviderConfigured_Should_Deserialize()
        {
            section.Should().NotBeNull();
            providers.Should().NotBeNull();
            providers.Providers.Should().NotBeNull();
        }

        [Fact]
        public void When_TwitterConfigured_Should_ReturnProvider()
        {
            providers.Providers.Should()
                .ContainSingle(p => p.Name == "Twitter");
        }

        #endregion Public Methods
    }
}