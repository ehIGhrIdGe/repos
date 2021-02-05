using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EChat.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EChat
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);

            ManagerDb.Configuration = Configuration;

            //AddAuthenticationメソッドには、使用する認証方式を文字列として渡す。
            //「CookieAuthenticationDefaults.AuthenticationScheme」は Cookie認証を示す文字列（定数）
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    //認証されていないユーザーがリソースにアクセスしようとしたときにリダイレクトする相対パス
                    options.LoginPath = new PathString("/Login/Index");

                    //アクセスが禁止されているリソースにアクセスしようとしたときにリダイレクトする相対パス
                    options.AccessDeniedPath= new PathString("/Chat/Index");
                    
                    //認証Cookieの有効期限を設定する。
                    //options.ExpireTimeSpan= TimeSpan.FromMinutes(60);
                    
                    //Cookieの有効期限が半分より過ぎたときにユーザーがログインをしていると、
                    //自動で有効期限を更新する
                    //options.SlidingExpiration= true;        
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    //template: "{controller=Chat}/{action=Index}/{id?}");
                    template: "{controller=Login}/{action=Index}/{id?}");
        });
        }
    }
}
