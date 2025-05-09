
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServiceLayer.Service;
using System.Text;
using TodoCore.InterfacesContract;
using TodoCore.ProfileMap;
using TodoCore.ServiceContract;
using TodoListAPI.Helper;
using TodoRepositry.Data.Contexts;
using TodoRepositry.ImplementaionRepo;

namespace TodoListAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDependency(builder.Configuration);
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowFrontend", policy =>
				{
					policy.AllowAnyOrigin()
						  .AllowAnyHeader()
						  .AllowAnyMethod();
				});
			}); var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
