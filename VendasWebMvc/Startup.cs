using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using VendasWebMvc.Models;
using VendasWebMvc.Data;
using VendasWebMvc.Services;

namespace VendasWebMvc
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<VendasWebMvcContext>(options =>
                    // Esse código foi usado para usar o banco de dados Msql --Substituir pelo código que estava antes
                    // Importante colocar o mesmo nome que está na classe "Context" do projeto, nome caso a classe context é
                    // "VendasWebMvcContext".
                    //Também é importante colocar o nome do projeto dentro do " MigractionsAssembly( nome do projeto aqui com "")
                    options.UseMySql(Configuration.GetConnectionString("VendasWebMvcContext"), 
                    builder => builder.MigrationsAssembly("VendasWebMvc")));

            // caminho para a classe de povoamento do banco de dados - Independente do Migration
            services.AddScoped<PovoarDB>();
            // o comando abaixo serve para permitir que o serviço possa ser injetado em outras classe
            services.AddScoped<ServiceVendedor>();
            services.AddScoped<ServiceDepartamento>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PovoarDB povoar)
        {

            var enUS = new CultureInfo("en-US");
            var opcaosDeLocalizacao = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            app.UseRequestLocalization(opcaosDeLocalizacao);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // comando adiconado para povoar a base de dados
                povoar.Enviar();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
