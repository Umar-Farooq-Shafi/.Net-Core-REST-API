using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commander.Data;
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
using Newtonsoft.Json.Serialization;

namespace Commander
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
            // Configuration for mysql db
            string dbConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CommanderContext>(
                opt => opt.UseMySql(dbConnectionString,
                ServerVersion.AutoDetect(dbConnectionString),
                options => {
                    options.EnableRetryOnFailure();
                })
            );

            // mapper configuration from model to data context
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Adding JSON serializer to controller
            services.AddControllers().AddNewtonsoftJson(
                s => s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Commander", Version = "v1" });
            });

            // Dependices injection our repository 
            // services.AddScoped<ICommanderRepo, MockCommanderRepo>(); // mock data repo
            services.AddScoped<ICommanderRepo, SqlCommanderRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Commander v1"));
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
