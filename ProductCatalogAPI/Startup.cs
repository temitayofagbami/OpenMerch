﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductCatalogAPI.Data;

namespace ProductCatalogAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //reads all configuration settings including appsetting JSON
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.Configure<CatalogSettings>(Configuration);

            services.AddMvc();

            // create  connectionString

           // services.AddDbContext<CatalogContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));


            //configure db
            //  services.AddDbContext<CatalogContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
          
            //create connectionstring

            var server = Configuration["DatabaseServer"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DatabaseUser"];
            var password = Configuration["DatabaseUserPassword"];
            var connectionString = String.Format("Server={0};Database={1};User={2};Password={3};", server, database, user, password);

            //configure connectionstring

           services.AddDbContext<CatalogContext>(

               options => options.UseSqlServer(connectionString));





            //ideally the CatalogSeed will be called here but it is an asynchronous call
            //might not finish before it leaves
            //so it will be called in Main


            // Add framework services.

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1",
                    new Swashbuckle.AspNetCore.Swagger.Info
                    {

                        Title = "OpenMerch - Product Catalog HTTP API",
                        Version = "v1",
                        Description = "The Product Catalog Microservice HTTP API. This is a Data-Driven/CRUD microservice sample",
                        TermsOfService = "Terms Of Service"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
           .UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint($"/swagger/v1/swagger.json", "ProductCatalog API V1");

           });
            app.UseMvc();
        }
    }
}
