namespace CartWebAPI
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleInjector;
    using SimpleInjector.Integration.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using System.IO;
    using System.Reflection;

#pragma warning disable S2325 // Methods and properties that don't access instance data should be static
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonFormatters()
                .AddApiExplorer();
            IntegrateSimpleInjector(services);
            EnableSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
#pragma warning disable S1075 // URIs should not be hardcoded
                c.SwaggerEndpoint("/swagger/doc/swagger.json", "Cart API");
#pragma warning restore S1075 // URIs should not be hardcoded

                c.RoutePrefix = "";
            });

            app.UseMvc();
        }

        private static void IntegrateSimpleInjector(IServiceCollection services)
        {
            var container = AppBootstrapper.InitializeDI();
            services.AddHttpContextAccessor();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        private static void EnableSwagger(IServiceCollection services)
        {
            var swaggerInfo = new Info
            {
                Title = "Ciceksepeti Cart Api",
                Version = "v1",
                Contact = new Contact
                {
                    Name = "Mehmet YILDIZ",
                    Email = "mehmet@yildizmehmet.net",
                    Url = "https://github.com/ishbara/cs"
                },
                Description = "Docker enabled, Redis backed simple Cart Api for CicekSepeti assesment.",
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("doc", swaggerInfo);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
#pragma warning restore S2325 // Methods and properties that don't access instance data should be static
}
