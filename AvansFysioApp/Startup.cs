using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using AvansFysioAppInfrastructure.Repos;

namespace AvansFysioApp
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
            services.AddScoped<IRepo, Repository>();
            services.AddScoped<PatientFileIRepo, PatientFileRepository>();
            services.AddScoped<IPhysiotherapistRepo, PhysiotherapistRepo>();
            services.AddScoped<OperationIRepo, OperationRepo>();
            services.AddScoped<DiagnosisIRepo, DiagnosisRepo>();
            services.AddScoped<TreatmentPlanIRepo, TreatmentPlanRepository>();
            services.AddScoped<TreatmentIRepo, TreatmentRepository>();
            services.AddScoped<AppointmentIRepo, AppointmentRepository>();
            services.AddScoped<SessionIRepo, SessionRepository>();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddDbContext<MasterDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MasterData")));
            services.AddDbContext<AccountDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Security")));
            services.AddIdentity<IdentityUser, IdentityRole>(config => {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AccountDbContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "AccountCookie";
                config.LoginPath = "/Account/Login";
            });

            services.AddAuthorization(options => options.AddPolicy("PhysiotherapistOnly", policy => policy.RequireClaim("Physiotherapist")));
            services.AddAuthorization(options => options.AddPolicy("InternOnly", policy => policy.RequireClaim("Intern")));
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Slug",
                    pattern: "Home",
                    defaults: new {controller = "Home", action = "Index"});

                endpoints.MapControllerRoute(
                    name: "Patients",
                    pattern: "Home/Patients",
                    defaults: new { controller = "Home", action = "PatientList" });

                endpoints.MapControllerRoute(
                    name: "EditPatient",
                    pattern: "Home/EditPatientView/{id}",
                    defaults: new { controller = "Home", action = "EditPatientView" });
                endpoints.MapControllerRoute(
                    name: "DetailView",
                    pattern: "Home/DetailView/{id}",
                    defaults: new { controller = "Home", action = "DetailView" });

                endpoints.MapControllerRoute(
                    name: "Login",
                    pattern: "Account/Login",
                    defaults: new { controller = "Account", action = "Login" });

                endpoints.MapControllerRoute(
                    name: "Register",
                    pattern: "Account/Register",
                    defaults: new { controller = "Account", action = "Register" });
            });

        }
    }
}
