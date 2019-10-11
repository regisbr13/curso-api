using curso_api.Business;
using curso_api.Business.Interfaces;
using curso_api.Data;
using curso_api.Data.VO;
using curso_api.Data.VO.Converters;
using curso_api.Data.VO.Converters.Interfaces;
using curso_api.Model;
using curso_api.Repository;
using curso_api.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using WebApiContrib.Core.Formatter.Csv;

namespace MinhaApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => {
                options.RespectBrowserAcceptHeader = true;      // permite que a Api aceite o tipo de formato enviado pelo client no header da requisição
                options.ReturnHttpNotAcceptable = true;         // especifica que a Api retornará 406 Not Acceptable caso o client passe no header um formato não suportado
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("csv", MediaTypeHeaderValue.Parse("text/csv"));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddXmlSerializerFormatters().AddCsvSerializerFormatters();
            services.AddDbContext<Context>(options => options.UseMySQL(Configuration.GetConnectionString("ConDb")));
            services.AddApiVersioning();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBusiness<PersonVO>, PersonBusiness>();
            services.AddScoped<IBusiness<BookVO>, BookBusiness>();
            services.AddScoped<PersonConverter>();
            services.AddScoped<BookConverter>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
