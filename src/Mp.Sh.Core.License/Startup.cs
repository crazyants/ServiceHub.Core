/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mp.Sh.Core.AspNet.Configurations;
using Newtonsoft.Json;
using Mp.Sh.Core.License.Services;
using Mp.Sh.Core.License.Models;
using Newtonsoft.Json.Serialization;
using Mp.Sh.Core.License.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.ActiveDirectory;
using System;

namespace Mp.Sh.Core.License
{
    public class Startup
    {
        #region Public Constructors

        /// <summary>
        /// Startup the web application and inject the correct configuration file 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Provides runtime configuration to the application 
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        #endregion Public Properties

        #region Public Methods

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // inject a CSP header to allow dynamic content from Polymer
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("Content-Security-Policy",
                                         "default-src 'self' * 'unsafe-inline' 'unsafe-eval' data:");
                await next();
            });

            // enable asp.net identiy BEFORE Identity Server
            app.UseIdentity();

            // bootstrap Indetity Server 4.0
            app.UseIdentityServer(); // inform that this is an identity server

            // cookie middleware for temporarily storing the outcome of the external authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                AutomaticAuthenticate = false,
                AutomaticChallenge = false,
                LoginPath = "/Account/Login"
            });

            // this cookie is used to authenticate into Identity Server itself
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = IdentityServerConstants.LocalIdentityProvider,
                AutomaticChallenge = true
            });

            var socialProvidersSection = Configuration.GetSection("SocialProviders").Get<SocialProviders>();

            if (socialProvidersSection != null)
            {
                foreach (var provider in socialProvidersSection.Providers)
                {
                    if (provider.Name.ToLower().Equals("twitter"))
                    {
                        app.UseTwitterAuthentication(new TwitterOptions
                        {
                            AuthenticationScheme = "Twitter",
                            SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                            ConsumerKey = provider.ClientId,
                            ConsumerSecret = provider.ClientKey
                        });
                    }

                    if (provider.Name.ToLower().Equals("facebook"))
                    {
                        app.UseFacebookAuthentication(new FacebookOptions
                        {
                            AuthenticationScheme = "Facebook",
                            SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                            AppId = provider.ClientKey,
                            AppSecret = provider.ClientId
                        });
                    }

                    if (provider.Name.ToLower().Equals("google"))
                    {
                        app.UseGoogleAuthentication(new GoogleOptions
                        {
                            AuthenticationScheme = "Google",
                            SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                            ClientId = provider.ClientId,
                            ClientSecret = provider.ClientKey
                        });
                    }

                    if (provider.Name.ToLower().Equals("linkedin"))
                    {
                        app.UseLinkedInAuthentication(new LinkedInOptions
                        {
                            AuthenticationScheme = "LinkedIn",
                            SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                            ClientId = provider.ClientId,
                            ClientSecret = provider.ClientKey
                        });
                    }

                    if (provider.Name.ToLower().Equals("microsoft"))
                    {
                        app.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions
                        {
                            AuthenticationScheme = "Microsoft",
                            SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                            ClientId = provider.ClientId,
                            ClientSecret = provider.ClientKey
                        });
                    }
                }
            }

            app.UseStaticFiles(); // configure static
            app.UseMvcWithDefaultRoute(); //  and mvc framework
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // entity framework for asp.net identity
            services
                .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            // identity models
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext, Guid>()
                .AddDefaultTokenProviders();

            // [asp.net core mvc]
            services
                .AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            // [asp.net identity services]
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // add identity server functionalities
            services
                .AddIdentityServer()
                .AddTemporarySigningCredential() // [temporary for development]
                .AddInMemoryIdentityResources(ConfigurationMockService.GetIdentityResources()) // [OIDC resources]
                .AddInMemoryApiResources(ConfigurationMockService.GetApiResources()) // apis to be protected
                .AddInMemoryClients(ConfigurationMockService.GetClients()) // clients available
                .AddAspNetIdentity<ApplicationUser>(); // users available
        }

        #endregion Public Methods
    }
}