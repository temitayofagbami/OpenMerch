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
using ProductCatalogAPI.Data;

namespace ProductCatalogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //adding of the dummy data in catalog seed
            //host is docker image
            var host = BuildWebHost(args);
            //give me a scope/instance of the docket image
            using (var scope = host.Services.CreateScope())

            {
                //get all the list of services on docker image
                var services = scope.ServiceProvider;

                //locate the catalog context out of the list of services
                var context = services.GetRequiredService<CatalogContext>();

                //add dummy data in catalog context, by calling Seed funtion 
                //call the wait on this async function becuse main cannot be made to async funtion
                CatalogSeed.SeedAsync(context).Wait();
            }
            //run docker image
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
