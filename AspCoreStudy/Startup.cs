using AspCoreStudy.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreStudy
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var strConexao = @"Host=localhost; Port=5432; User Id=postgres; Password=a1b2c3d4; Database=Mimic;";
            services.AddDbContext<Contexto>(opt =>
            {
                //opt.UseSqlite("Data Source=Database\\Mimic.db");
                opt.UseNpgsql(strConexao);
            });
            services.AddMvc(ops => { ops.EnableEndpointRouting = false; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
