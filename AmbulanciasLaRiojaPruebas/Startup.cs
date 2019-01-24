using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AmbulanciasLaRiojaPruebas
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //
            // Registro SenecaServicesSettings de appsettings.json
            services.Configure<FerrovialSettings.SenecaServiceSettings>(Configuration.GetSection("SenecaServicesSettings"));
            // --- ger ---
            // Resgistro HttpClientFactory (nuevo core 2.1) 
            // Se carga en el controlador de la misma forma que la configuración de AppSettings
            services.AddHttpClient();

            // CORS Configuration
            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", builder =>
                {
                    builder
                          .AllowAnyOrigin()
                          .AllowAnyMethod();
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
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
