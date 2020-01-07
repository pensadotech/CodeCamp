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
            // Temporary element when testing deployment, restore SqlRestaurantData for final version
            services.AddScoped<ICampRepository, InMemoryCampRepository>();

            // AutoMapper: Define mapping profile
            var mappingCfg = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AllowNullCollections = true;
                mc.AllowNullDestinationValues = true;
          
            });

            // AutoMapper: Add AutoMapper as a singletion services
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
