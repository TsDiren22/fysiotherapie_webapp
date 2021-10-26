using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using AvansFysioAppInfrastructure.Repos;
using Microsoft.EntityFrameworkCore;

namespace AvansPhysioAppWebAPI
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

            services.AddControllers();
            services.AddDbContext<MasterDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MasterData")));
            services.AddScoped<OperationIRepo, OperationRepo>();
            services.AddScoped<DiagnosisIRepo, DiagnosisRepo>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AvansPhysioAppWebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AvansPhysioAppWebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "Get",
                    pattern: "Diagnosis/{id}",
                    defaults: new { controller = "DiagnosisController", action = "Get" });
                endpoints.MapControllerRoute(
                    name: "Get",
                    pattern: "Operation/{id}",
                    defaults: new { controller = "OperationController", action = "Get" });
            });
        }
    }
}
