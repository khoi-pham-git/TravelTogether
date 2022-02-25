using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TravelTogether2.Models;

namespace TravelTogether2
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
            services.AddMvc();

            //Fetching Connection string from APPSETTINGS.JSON  
            var ConnectionString = Configuration.GetConnectionString("MbkDbConstr");
            //Entity Framework  
            services.AddDbContext<TourGuide_v2Context>(options => options.UseSqlServer(ConnectionString));

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Travel Together API",
                        Description = "Travel Together Core Web API ",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "",
                            Url = new Uri("https://example.com/contact")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "",
                            Url = new Uri("https://example.com/license")
                        }
                    });


                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });

            //Enable cors
            services.AddCors();


            //
           


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Enable CORS
            app.UseCors(options => options.WithOrigins("http://localhost:3000", "http://http://traveltogetherr.somee.com", "http://localhost:3001").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            //Swagger
          



            if (env.IsDevelopment()|| env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelTogether v1"));
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
