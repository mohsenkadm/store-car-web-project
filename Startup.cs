using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using store_car_web_project.Classes;
using store_car_web_project.Models;
using store_car_web_project.Models.IServices;
using store_car_web_project.Models.Services;

namespace store_car_web_project
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        private Keys getkeys { get; }
        public Startup(IConfiguration configuration)
        {
            getkeys = new Keys();
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            ///memory catch
            services.AddMemoryCache();
            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                byte[] symmetricKey = Convert.FromBase64String(new Keys().SecretKey);
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(symmetricKey);
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = securityKey,
                    ValidIssuer = "PBlogsSite",
                    ValidAudience = "Subscriber",
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.FromDays(10),
                };
            });
            services.AddAuthorization();
            ///connection
            services.AddDbContext<PblogsContext>(option => option.UseSqlServer(getkeys.connectionstring));
            //interface
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IPostsServices, PostsServices>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
