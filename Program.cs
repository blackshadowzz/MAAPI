
using MAAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace MAAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            var connections = builder.Configuration.GetConnectionString("Connection");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connections);
            });
            builder.Services.AddCors(option => option.AddPolicy("AllowCore", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowCore");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
