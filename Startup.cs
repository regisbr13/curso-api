﻿using System;
using System.IO;
using System.Linq;
using curso_api.Business;
using curso_api.Business.Interfaces;
using curso_api.Data;
using curso_api.Data.VO;
using curso_api.Data.VO.Converters;
using curso_api.Data.VO.Converters.Interfaces;
using curso_api.Hypermedia;
using curso_api.Model;
using curso_api.Repository;
using curso_api.Repository.Interfaces;
using curso_api.Security;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Swagger;
using Tapioca.HATEOAS;
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
            }).AddXmlSerializerFormatters().AddCsvSerializerFormatters();
            services.AddDbContext<Context>(options => options.UseMySQL(Configuration.GetConnectionString("ConDb")));
            services.AddApiVersioning();
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEricher());
            services.AddSingleton(filterOptions);
            // DEPENDECY INJECTION
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<PersonBusiness>();
            services.AddScoped<IBusiness<BookVO>, BookBusiness>();
            services.AddScoped<ILoginBusiness, LoginBusiness>();
            services.AddScoped<PersonConverter>();
            services.AddScoped<BookConverter>();
            services.AddRouting();
            // DOCUMENTATION
            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new Info {Title = "RESTful API with ASP.NET Core", Version = "v1"});
                x.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddCors();
            // SECURITY
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);
            
            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                var paramsValidation = options.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(options => 
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c => 
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(x => {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}"
                );
            });
        }
    }
}
