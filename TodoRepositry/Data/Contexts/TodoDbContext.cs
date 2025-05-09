using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TodoCore.Entities;
namespace TodoRepositry.Data.Contexts
{
	public class TodoDbContext:DbContext
	{
		public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
		public DbSet<Todo> Todos { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly
				(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}
	}
}
