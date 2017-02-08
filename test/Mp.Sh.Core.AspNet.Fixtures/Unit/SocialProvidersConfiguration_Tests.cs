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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Mp.Sh.Core.AspNet.Fixtures.Unit
{
    public class SocialProvidersConfiguration_Tests : IDisposable
    {
        #region Private Fields

        private readonly IConfigurationRoot configuration;
        private readonly ITestOutputHelper output;
        private readonly SocialProviders providers;
        private readonly IConfigurationSection section;

        #endregion Private Fields

        #region Public Constructors

        public SocialProvidersConfiguration_Tests(ITestOutputHelper output)
        {
            this.output = output;

            var builder = new ConfigurationBuilder()
                .SetBasePath(new FileInfo(typeof(SocialProvidersConfiguration_Tests).GetTypeInfo().Assembly.Location).Directory.FullName)
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
        [Trait("Category", "Libraries")]
        public void SocialProviders_Should_Contain_ClientId()
        {
            providers.Providers.First()
                .ClientId.Should().Be("12345-ABCDE");
        }

        [Fact]
        [Trait("Category", "Libraries")]
        public void SocialProviders_Should_Contain_ClientKey()
        {
            providers.Providers.First()
                .ClientKey.Should().Be("99999-XXXXX");
        }

        [Fact]
        [Trait("Category", "Libraries")]
        public void SocialProviders_Should_Contain_Facebook()
        {
            providers.Providers.Should()
                .ContainSingle(p => p.Name == "Facebook");
        }

        [Fact]
        [Trait("Category", "Libraries")]
        public void SocialProviders_Should_Contain_Google()
        {
            providers.Providers.Should()
                .ContainSingle(p => p.Name == "Google");
        }

        [Fact]
        [Trait("Category", "Libraries")]
        public void SocialProviders_Should_Contain_LinkedIn()
        {
            providers.Providers.Should()
                .ContainSingle(p => p.Name == "LinkedIn");
        }

        [Fact]
        [Trait("Category", "Libraries")]
        public void SocialProviders_Should_Contain_Twitter()
        {
            providers.Providers.Should()
                .ContainSingle(p => p.Name == "Twitter");
        }

        [Fact]
        [Trait("Category", "Libraries")]
        public void SocialProviders_Should_Deserialize()
        {
            section.Should().NotBeNull();
            providers.Should().NotBeNull();
            providers.Providers.Should().NotBeNull();
        }

        #endregion Public Methods
    }
}