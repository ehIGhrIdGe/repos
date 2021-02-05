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

            //AddAuthentication���\�b�h�ɂ́A�g�p����F�ؕ����𕶎���Ƃ��ēn���B
            //�uCookieAuthenticationDefaults.AuthenticationScheme�v�� Cookie�F�؂�����������i�萔�j
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    //�F�؂���Ă��Ȃ����[�U�[�����\�[�X�ɃA�N�Z�X���悤�Ƃ����Ƃ��Ƀ��_�C���N�g���鑊�΃p�X
                    options.LoginPath = new PathString("/Login/Index");

                    //�A�N�Z�X���֎~����Ă��郊�\�[�X�ɃA�N�Z�X���悤�Ƃ����Ƃ��Ƀ��_�C���N�g���鑊�΃p�X
                    options.AccessDeniedPath= new PathString("/Chat/Index");
                    
                    //�F��Cookie�̗L��������ݒ肷��B
                    //options.ExpireTimeSpan= TimeSpan.FromMinutes(60);
                    
                    //Cookie�̗L���������������߂����Ƃ��Ƀ��[�U�[�����O�C�������Ă���ƁA
                    //�����ŗL���������X�V����
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
