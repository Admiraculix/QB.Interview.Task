using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QB.API.AutoMapper.Profiles;
using QB.API.WebMiddlewares;
using QB.Application.AutoMapper.Profiles;
using QB.Application.Configurations;
using QB.Application.Extensions;
using QB.External.Rest.Service.Extensions;
using QB.Persistence.Extensions;
using System;
using System.IO;
using System.Reflection;

namespace QB.Presentation
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

            services.RegisterPersistenceDepenencies(Configuration);
            services.RegisterApplicationServiceDepenencies();
            services.RegisterExternalRestServiceDepenencies();

            RegisterAutoMapper(services);
            RegisterConfigurations(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "QB Interview API",
                    Description = "A simple example ASP.NET Core Web API with Clean Architecture",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Georgi Bozhilov",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/georgibozhilov/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QB Interview API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(
                (cfg) =>
                {
                    cfg.AddCollectionMappers();
                },
                typeof(DtoToEntityProfile).GetTypeInfo().Assembly,
                typeof(DtoToResponseProfile).GetTypeInfo().Assembly);
        }

        private void RegisterConfigurations(IServiceCollection services)
        {
            services.AddSingleton(Configuration.
                GetSection("CountryNameVariation")
                .Get<CountryNameVariationConfiguration>(c => c.BindNonPublicProperties = true));
        }
    }
}
