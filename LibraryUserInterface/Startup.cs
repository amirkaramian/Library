using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ExamLibrary.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExamLibrary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpClient<IRequestService, RequestService>(client =>
            {
                ConfigHttpClient(client, "Library");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private void ConfigHttpClient(HttpClient client, string name)
        {
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            var baseAddress = Configuration.GetSection(name + "Url")?.Value;
            if(!string.IsNullOrEmpty(baseAddress))
                client.BaseAddress = new Uri(baseAddress);
//this code provide for Authorization by AMIR

//            var bearerToken = _httpContextAccessor.HttpContext.Request
//                .Headers["Authorization"]
//                .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));
//            if (bearerToken != null)
//            {
//                client.DefaultRequestHeaders.Add("Authorization", bearerToken);
//            }
        }
    }
}
