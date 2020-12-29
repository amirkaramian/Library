using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamLibrary.Service;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace LibraryApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        [Obsolete]
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;            
            Configuration = new ConfigurationBuilder()
               .SetBasePath(environment.ContentRootPath)
               .AddJsonFile("appsettings.json")
               .Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Library.Api", Version = "v1" });
            });
            services.AddDbContext<LibararyDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("LibraryConnection")));

            services.AddScoped<ILibraryService, LibraryService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hsa.Api");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
