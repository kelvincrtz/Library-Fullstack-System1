﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryFullstackSystem1.Data;
using Microsoft.EntityFrameworkCore;
using LibraryFullstackSystem1.Services;

namespace LibraryFullstackSystem1
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
            services.AddMvc();
            services.AddSingleton(Configuration);

            services.AddScoped<ILibraryAsset, AssetService>();
            services.AddScoped<ICheckout, LibraryCheckoutService>();
            services.AddScoped<ILibraryBranch, LibraryBranchService>();
            services.AddScoped<IPatron, LibraryPatronService>();
            services.AddScoped<ILibraryCard, LibraryCardService>();
            services.AddScoped<IHold, HoldService>();

            services.AddDbContext<LibrarySystemDbContext>(options =>
                                options.UseSqlServer(Configuration.GetConnectionString("LibraryConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
