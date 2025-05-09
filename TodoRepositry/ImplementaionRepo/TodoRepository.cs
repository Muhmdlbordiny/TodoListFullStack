using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoCore.Entities;
using TodoCore.InterfacesContract;
using TodoRepositry.Data.Contexts;

namespace TodoRepositry.ImplementaionRepo
{
	public class TodoRepository : ITodoRepositry
	{
		private readonly TodoDbContext _context;

		public TodoRepository(TodoDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Todo>> GetAllAsync(TodoStatus? status, TodoPriority? priority)
		{
			var query = _context.Todos.AsQueryable();
			if (status.HasValue)
				query = query.Where(t => t.Status == status.Value);
			if(priority.HasValue)
				query = query.Where(p=>p.Priority==priority.Value);
			return await query.ToListAsync();
		}

		public async Task<Todo?> GetByIdAsync(Guid id) =>
			await _context.Todos.FindAsync(id);

		public async Task<Todo> AddAsync(Todo todo)
		{
			_context.Todos.Add(todo);
			await _context.SaveChangesAsync();
			return todo;
		}

		public async Task<Todo?> UpdateAsync(Todo todo)
		{
			var existing = await _context.Todos.FindAsync(todo.Id);
			if (existing == null) return null;

			_context.Entry(existing).CurrentValues.SetValues(todo);
			await _context.SaveChangesAsync();
			return existing;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var todo = await _context.Todos.FindAsync(id);
			if (todo == null) return false;
			_context.Todos.Remove(todo);
			await _context.SaveChangesAsync();
			return true;
		}
	}


}
