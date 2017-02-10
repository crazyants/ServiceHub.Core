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

            // inject a CSP header to allow dynamic content from Polymer
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("Content-Security-Policy",
                                         "default-src 'self' * 'unsafe-inline' 'unsafe-eval' data:");
                await next();
            });

            // bootstrap Indetity Server 4.0
            app.UseIdentityServer(); // inform that this is an identity server

            foreach (var provider in Configuration.GetSection("SocialProviders").Get<SocialProviders>().Providers)
            {
                if (provider.Name.ToLower().Equals("twitter"))
                {
                    app.UseTwitterAuthentication(new TwitterOptions
                    {
                        AuthenticationScheme = "Twitter",
                        SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                        ConsumerKey = provider.ClientKey,
                        ConsumerSecret = provider.ClientId
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
            }

            // cookie middleware for temporarily storing the outcome of the external authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                AutomaticAuthenticate = false,
                AutomaticChallenge = true,
                LoginPath = "/Account/Login"
            });

            app.UseStaticFiles(); // configure static
            app.UseMvcWithDefaultRoute(); //  and mvc framework
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // [asp.net core mvc]
            services
                .AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });
            // add identity server functionalities
            services
                .AddIdentityServer()
                .AddTemporarySigningCredential() // [temporary for development]
                .AddInMemoryIdentityResources(Config.GetIdentityResources()) // [OIDC resources]
                .AddInMemoryApiResources(Config.GetApiResources()) // apis to be protected
                .AddInMemoryClients(Config.GetClients()) // clients available
                .AddTestUsers(Config.GetUsers()); // users available
        }

        #endregion Public Methods
    }
}