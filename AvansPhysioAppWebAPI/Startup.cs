using System.Security;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using AvansFysioAppInfrastructure.Repos;
using AvansPhysioAppWebAPI.GraphQl;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            services.AddDbContext<AccountDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Security")));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Reduce the complexity rules. Not because this is best practice, but simply to allow 
                // an user to be created with a simple password for testing.
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AccountDbContext>().AddDefaultTokenProviders();
            // Configure JWT usage.
            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.ValidateIssuer = false;
                options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["BearerTokens:Key"]));
            });
            services.AddScoped<OperationIRepo, OperationRepo>();
            services.AddScoped<IDiagnosisRepo, DiagnosisRepo>();
            services.AddScoped<GraphQlQueries>();
            services.AddGraphQLServer().AddType<DiagnosisType>().AddQueryType<GraphQlQueries>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "Get",
                    pattern: "Diagnosis/{id}",
                    defaults: new { controller = "Diagnosis", action = "Get" });
                endpoints.MapControllerRoute(
                    name: "Get",
                    pattern: "Operation/{id}",
                    defaults: new { controller = "Operation", action = "Get" });
                endpoints.MapGraphQL();
            });

            app.UsePlayground(new PlaygroundOptions
            {
                QueryPath = "/api",
                Path = "/playground"
            }); 
        }
    }
}
