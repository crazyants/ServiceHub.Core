/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Mp.Sh.Core.OData
{
    public class Program
    {
        #region Public Methods

        public static void Main(string[] args)
        {
            Console.Title = "OData Core | Mproof";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:82")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        #endregion Public Methods
    }
}