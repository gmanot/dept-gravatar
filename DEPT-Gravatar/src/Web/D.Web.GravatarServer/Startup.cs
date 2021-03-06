﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using D.Web.GravatarServer.Middleware.Gravatar;

namespace D.Web.GravatarServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            StaticFileOptions staticFileOptions = new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers["Cache-Control"] = "no-cache";
                    //context.Context.Response.Headers["Expires"] = DateTime.UtcNow.AddHours(12).ToString("R");
                }
            };

            GravatarOptions gravatarOptions = new GravatarOptions()
            {
                DefaultGravatarPath = "/avatar/default-avatar.jpg"
            };
            //Handle the /gravatar/ path and apply a not found fallback
            app.UseGravatarMiddleware(gravatarOptions);

            app.UseStaticFiles(staticFileOptions);

            app.Run((context) =>
            {
                context.Response.StatusCode = 404;
                return Task.FromResult(0);
            });
        }
    }
}
