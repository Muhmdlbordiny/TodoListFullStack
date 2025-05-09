using Microsoft.EntityFrameworkCore;
using ServiceLayer.Service;
using TodoCore.InterfacesContract;
using TodoCore.ProfileMap;
using TodoCore.ServiceContract;
using TodoRepositry.Data.Contexts;
using TodoRepositry.ImplementaionRepo;

namespace TodoListAPI.Helper
{
	public static class RegisterDependencies
	{
		public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddBuiltinService();
			services.AddSwagerService();
			services.AddDbcontextService(configuration);
			services.AddTodoDefinedService();
			services.AddAutoMapperService();
			return services;
		}
		private static IServiceCollection AddBuiltinService(this IServiceCollection services)
		{
			services.AddControllers();

			return services;
		}
		private static IServiceCollection AddSwagerService(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			return services;
		}
		private static IServiceCollection AddDbcontextService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<TodoDbContext>
				(options =>
				{
					options.UseSqlServer
					(
						configuration.
						GetConnectionString("DefaultConnection")
					 );
				}
				);
			return services;
		}
		private static IServiceCollection AddTodoDefinedService(this IServiceCollection services)
		{
			services.AddScoped<ITodoRepositry, TodoRepository>();
			services.AddScoped<ITodoService, TodoService>();
			return services;
		}
		private static IServiceCollection AddAutoMapperService(this IServiceCollection services)
		{
			services.AddAutoMapper(
				T => T.AddProfile(new TodoMappingProfile())
				);
			return services;
		}



	}
}
