using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using simple_note.Auth;
using simple_note.Middlewares;
using simple_note.Note;
using simple_note.User;

namespace simple_note
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
            services.AddCors();

            services.AddDbContext<SimpleTodoEntities>(
                options => options.UseMySQL(Configuration.GetConnectionString("Default"))
            );
            services.AddControllers();

            services.AddScoped<JwtService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<NoteService>();
            services.AddScoped<AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(options => options
                .AllowAnyOrigin()
                //.AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
                );

            app.UseJWTMiddleware();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
