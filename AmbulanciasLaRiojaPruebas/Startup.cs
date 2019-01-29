using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace AmbulanciasLaRiojaPruebas
{
    /// <summary>
    /// MUY IMPORTANTE PARA PUBLICAR CON SWAGGER
    /// Hay que habilitar tanto en RELEASE como en DEBUG:
    /// Propiedades / Compilación / Salida / Archivo de documentación XML
    /// </summary>
    public class Startup
    {/// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //
            // Registro SenecaServicesSettings de appsettings.json
            services.Configure<FerrovialSettings.SenecaServiceSettings>(Configuration.GetSection("ServiceSettings"));
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

            var ServiceSettings = Configuration.GetSection("ServiceSettings").Get<FerrovialSettings.SenecaServiceSettings>();

            services.AddMvc()
            .AddJsonOptions(options =>
            {
                 // --- ger ---
                 // Remove null fields from API JSON response
                 options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
                 // Format result PascalCase, comentar para camelCase
                 options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                        Version = ServiceSettings.Version,
                        Title = "Ambulancias La Rioja API",
                        Description = "Ambulancias La rioja Web API",
                        TermsOfService = "None" //,
                        // Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Contact Example", Email = "contact.example@example.com", Url = "https://example.com/example" },
                        // License = new Swashbuckle.AspNetCore.Swagger.License { Name = "Use under LICX", Url = "https://example.com/license" } 
                });

            // Set the comments path for the Swagger JSON and UI.
            // El fichero fts.xml debe estar en el raiz y forzar publicación, no lo copia de de bin/Release
            string basePath = System.AppContext.BaseDirectory; // string basePath = PlatformServices.Default.Application.ApplicationBasePath;
                string xmlPath = System.IO.Path.Combine(basePath, "AmbulanciasLaRiojaPruebas.xml");
                c.IncludeXmlComments(xmlPath);

                // Para habilitar tags específicas de Swagger
                c.EnableAnnotations();
                // --- ger ---
                // Extra Authorization TextBox in swagger where you can add your token in the format 'Bearer {jwttoken}'
                //services.ConfigureSwaggerGen(options =>
                //{
                //    options.OperationFilter<AmbulanciasLaRiojaPruebas.Swagger.AuthorizationHeaderParameterOperationFilter>();
                //});
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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
            // --- ger ---
            // CORS Support
            // it must be placed before UseMvc and before or after
            // UseStaticFiles, depending on if you want the static files to be 
            // Cors enabled or not 
            app.UseCors("AnyOrigin");

            // --- ger ---
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                // --- ger ---
                // adding a relative path to swagger.json
                // Para que funcione en rutas distintas del raíz (http://w10gerardo/netcore/ferro_geocoder/ p.ej)
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Ambulancias La Rioja API V1");
                c.DocumentTitle = "Ambulancias La Rioja API";
                c.InjectStylesheet("../css/swagger.min.css");
                // Para quitar la barra swagger
                c.InjectStylesheet("../swagger-ui/custom.css");
                // Todos los tags de la pantalla de inicio sin expandir
                c.DocExpansion(DocExpansion.None);
            });

            // --- ger ---
            // Default static index page
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new List<string> { "index.html" }
            });

            // --- ger ---
            // Enable use static files in wwwroot
            app.UseStaticFiles();

            // --- ger ---
            // Enable Authentication
            app.UseAuthentication();

            // --- ger ---
            // Redirecciona a HTTPS siempre
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
