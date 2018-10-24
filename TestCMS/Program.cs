using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestCMS.Models;

namespace TestCMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();
 
      
 
            host.Run();
        }
 
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)  .ConfigureAppConfiguration((hostingContext, config) =>
                {
              //      config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("Properties/appsettings.json", optional: false, reloadOnChange: false);

                })
                .UseStartup<Startup>();
    }
}