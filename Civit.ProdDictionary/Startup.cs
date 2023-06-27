using Civit.ProdDictionary.Business;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Civit.Global;
using Civit.ProdDictionary.Host.Context;
using Microsoft.EntityFrameworkCore;
using Civit.ProdDictionary.Host.Repository;
using Civit.ProdDictionary.Host.Business;

namespace Civit.ProdDictionary
{
    public class Startup
    {        
        public Startup(IConfiguration configuration)
        {            
            Configuration = configuration;
            ConfigReadFunction.SetConfiguration(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProdDictionaryDbContext>
                (options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));

            services.AddControllers();
            services.AddScoped<IProdDictionaryAFL, ProdDictionaryBLL>();
            services.AddScoped<IProdDictionaryRepo, ProdDictionaryRepo>();
            services.AddScoped<IProdDictionaryDbAFL, ProdDictionaryDbBLL>();
            //services.AddAuthentication();

            services.AddHttpContextAccessor();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            //services.ConfigureLocalization<Startup, ProdDictionaryResource>("/Localization");

            //services.AddAuthentication("Bearer")
            //.AddJwtBearer(options =>
            //{
            //    options.Authority = Configuration["AuthServer:Authority"];
            //    options.RequireHttpsMetadata = Convert.ToBoolean(Configuration["AuthServer:RequireHttpsMetadata"]);
            //    options.Audience = "Civit";
            //    options.BackchannelHttpHandler = new HttpClientHandler
            //    {
            //        ServerCertificateCustomValidationCallback =
            //            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            //    };
            //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            //    {
            //        ValidAudience = "Civit",                   
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});


            //Authorization Configuration
            services.AddAuthentication("Bearer")
               .AddIdentityServerAuthentication(options =>
               {
                   // base-address of your identityserver
                   options.Authority = ConfigReadFunction.ConfigRead<string>("AuthServer:Authority");

                   // name of the API resource
                   options.ApiName = ConfigReadFunction.ConfigRead<string>("AuthServer:ApiResource");
                   options.ApiSecret = ConfigReadFunction.ConfigRead<string>("AuthServer:ApiResourceSecret");
               });


            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service  
                // note: the specified format code will format the version as "'v'major[.minor][-status]"  
                options.GroupNameFormat = "VV";
                options.SubstitutionFormat = "VV";
                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat  
                // can also be used to control the format of the API version in route templates  
                options.SubstituteApiVersionInUrl = true;
            });


            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Civit API", Version = "v1" });
                    options.TagActionsBy(api =>
                    {
                        if (api.GroupName != null)
                        {
                            return new[] { api.GroupName };
                        }

                        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                        if (controllerActionDescriptor != null)
                        {
                            return new[] { controllerActionDescriptor.ControllerName };
                        }

                        throw new InvalidOperationException("Unable to determine tag for endpoint.");
                    });
                    options.DocInclusionPredicate((name, api) => true);                               

                    options.EnableAnnotations();                  

                });


            services.AddCors(options =>
            {
                options.AddPolicy("Default", policy =>
                {
                    policy
                    .WithOrigins(Configuration["Origins"].Split(","))
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {                
               
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Civit.ProdDictionary v1"));
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                if(exception is Exception)
                {                    
                    var response = new { error = new { code = "", message = exception.Message, details = "", data = ""} };
                    await context.Response.WriteAsJsonAsync(response);
                    context.Response.StatusCode = 403;
                }
                
            }));

            app.UseCors("Default");
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
