using CDBCalculator.Infrastructure.DependencyInjection;
using CDBCalculator.Infrastructure.Midleware;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Mvc;
namespace CDBCalculator.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddDependencyInjection();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAuthentication(
                    CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate();
           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseHealthChecks("/api/health");
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(); 
            }
            else
            {
                app.UseHsts(); 
            }

            app.UseHttpsRedirection(); 
            app.UseRouting(); 

            app.UseAuthorization(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });
        }

    }
}
