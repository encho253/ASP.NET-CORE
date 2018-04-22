using CatsServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CatsServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options =>
                options.UseSqlServer("Server=.;DataBase=CatsServerDb;Integrated Security=true"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");

                return next();
            });

            app.MapWhen(ctx => ctx.Request.Path.Value == string.Empty && ctx.Request.Method == "GET",
                home =>
                {
                    home.Run(async (context) =>
                    {
                        await context.Response.WriteAsync($"<h1>{env.ApplicationName}</h1>");

                        var db = context.RequestServices.GetService<CatsDbContext>();

                        var catData = db.Cats.Select(c => new
                        {
                            c.Id,
                            c.Name
                        }).ToList();

                        await context.Response.WriteAsync("<ul>");

                        foreach (var cat in catData)
                        {
                            await context.Response.WriteAsync($@"<li><a href=""/cats/{cat.Id}"">{cat.Name}</li>");
                        }

                        await context.Response.WriteAsync("</ul>");
                        await context.Response.WriteAsync(@"
                            <form action=""/cat/add"">
                                <input type=""submit"" value=""Add Cat"" />
                            <form>");
                    });
                });

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("404 Page was not found :/");
            });
        }
    }
}