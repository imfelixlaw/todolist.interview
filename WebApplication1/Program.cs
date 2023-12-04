using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Ef.Models;
using WebApplication1.Services.Daos;
using WebApplication1.Services.Interfaces;
using WebApplication1.Services.Managers;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITodoService, TodoService>();

            builder.Services.AddScoped<UserService>();


            builder.Services.AddAuthentication("BasicAuthentication")
        .       AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            builder.Services.AddDbContext<todoContext>(
                options =>
                {
                    options.UseSqlServer("Data Source=172.16.10.21;Initial Catalog=todo;User ID=sa;Password=Password1;TrustServerCertificate=True");
                });
            builder.Services.AddScoped<DbContext, todoContext>();
            builder.Services.AddScoped<UserDaos, UserDaos>();
            builder.Services.AddScoped<TodoDaos, TodoDaos>();


            var app = builder.Build();

            {
                // global cors policy
                app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                // custom basic auth middleware
                app.UseMiddleware<BasicAuthMiddleware>();

                app.MapControllers();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
