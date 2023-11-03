using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using TransportManagementCore.Middleware;

namespace TransportManagementCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DBHelper.DBHelper.ConnectionString = Configuration.GetSection("Settings:ConnectionString").Value;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // this line is depended to nuget package: Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation /
            services.AddMvc().AddControllersAsServices();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddSignalR();
            services.AddControllersWithViews();
            //services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            // IHttpContextAccessor is no longer wired up by default, you have to register it yourself /
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = "LoginId";
            //    options.Cookie.Name = "BranchId";
            //    options.IdleTimeout = TimeSpan.FromHours(2);
            //    options.Cookie.IsEssential = true;
            //});
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            // Add this line to enable session handling

            // Add other services
            services.AddMvc();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            //services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
            //{
            //    options.ValueCountLimit = int.MaxValue;
            //    options.ValueLengthLimit = int.MaxValue;
            //});

            ////services.AddControllersWithViews().AddNewtonsoftJson(o => { o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; o.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()); });
            //services.AddControllersWithViews()
            //.AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseStaticFiles();

            app.UseWhen(context => !context.Request.Path.Value.ToLower().Equals("/login") && !context.Request.Path.Value.ToLower().Equals("/signin"), appBuilder =>
            {
                appBuilder.UseSessionCheckMiddleware();
            });

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();

        }
    }
}
