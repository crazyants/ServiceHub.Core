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

            app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
            }

            app.UseIdentityServer(); // inform that this is an identity server

            // cookie middleware for temporarily storing the outcome of the external authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                AutomaticAuthenticate = false,
                AutomaticChallenge = true,
                LoginPath = "/Account/Login"
            });

            // middleware for linkedin authentication
            app.UseLinkedInAuthentication(new LinkedInOptions
            {
                AuthenticationScheme = "LinkedIn",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "75sjisj8h5uj9t",
                ClientSecret = "Fnm7BtgJn0pM9oLR"
            });

            // middleware for facebook authentication
            app.UseFacebookAuthentication(new FacebookOptions
            {
                AuthenticationScheme = "Facebook",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                AppId = "2031068163786332",
                AppSecret = "7cb47bee7ce8d9789389a34795f17a0c"
            });

            // middleware for twitter authentication
            app.UseTwitterAuthentication(new TwitterOptions
            {
                AuthenticationScheme = "Twitter",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ConsumerKey = "aYQw0HeXceMtCuuuH4qlL4LNi",
                ConsumerSecret = "JieZI9HTNDJNbI4OEZbNolnBHtD6aywqtN22WhqQk9VZyUYXlR",
            });

            // middleware for google authentication
            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com",
                ClientSecret = "wdfPY6t8H8cecgjlxud__4Gh"
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