/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mp.Sh.Core.Front
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
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
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

            /* enable cookies authentication */
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true // this consents Indentity Server to logout also the client
            });

            /* JWT needs to be turned off because we use "well-known ID"
             * Then we point to Security Project and specify the Client Id used to connect
             *  */
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = "http://localhost:83",
                RequireHttpsMetadata = false,

                ClientId = "mvc_client", // it requires ClientID
                ClientSecret = "secret", // and Client Secret used also in backend
                ResponseType = "code id_token", // returns all required claims in the User.Identity object
                Scope = { "odata", "offline_access", "email", "address" }, // allow odata and refresh
                GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true
            });

            app.UseStaticFiles(); // configure static
            app.UseMvcWithDefaultRoute(); //  and mvc framework
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }); // [asp.net core mvc]
        }

        #endregion Public Methods
    }
}