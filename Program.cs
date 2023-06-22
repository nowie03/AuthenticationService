

using AuthenticationService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AuthenticationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //add db context
            builder.Services.AddDbContext<ServiceContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("local-server")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            
                app.UseSwagger();
                app.UseSwaggerUI();
            

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}