using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCamp.Data.Repositories;
using CodeCamp.Domain.Repositories;
using CodeCampApp.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeCampApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DEV ONLY: For development, instantiate a singleton that will implement
            // the ICampRepository using the InMemoryCampRepository with dummy data
            // Note: singleton is not good for prod envitonment
            services.AddSingleton<ICampRepository, InMemoryCampRepository>();

            // TEST change 

            // AutoMapper: Define a mapping profile. In this case, the App.MappingProfile. 
            // It contains the mapping definition between Domain.DTOs and App.Models
            // helping to copy data between Domain.DTOs and App.Models, avoiding teh front end
            // to manipulate directly data from the DB and to simplify the handling of data
            // when it comes from more than one DTO. The model will help consolidate many DTO 
            // data into as single model.
            var mappingCfg = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AllowNullCollections = true;
                mc.AllowNullDestinationValues = true;
            });

            // AutoMapper: Add IMapper as a singletion services
            IMapper mapper = mappingCfg.CreateMapper();
            services.AddSingleton(mapper);


            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
