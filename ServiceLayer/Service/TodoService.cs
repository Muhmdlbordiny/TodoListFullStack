using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoCore.Dtos;
using TodoCore.Entities;
using TodoCore.InterfacesContract;
using TodoCore.ServiceContract;
using TodoRepositry.Data.Contexts;
using TodoRepositry.ImplementaionRepo;

namespace ServiceLayer.Service
{
	public class TodoService : ITodoService
	{
		private readonly ITodoRepositry _repository;

		public TodoService(ITodoRepositry repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Todo>> GetTodosAsync(TodoStatus? status, TodoPriority? priority) =>
			await _repository.GetAllAsync(status,priority);

		public async Task<Todo?> GetTodoByIdAsync(Guid id) =>
			await _repository.GetByIdAsync(id);

		public async Task<Todo> CreateTodoAsync(Todo todo)
		{
			todo.CreatedDate = DateTime.UtcNow;
			todo.LastModifiedDate = DateTime.UtcNow;
			return await _repository.AddAsync(todo);
		}

		public async Task<Todo?> UpdateTodoAsync(Todo todo)
		{
			todo.LastModifiedDate = DateTime.UtcNow;
			return await _repository.UpdateAsync(todo);
		}

		public async Task<bool> DeleteTodoAsync(Guid id) =>
			await _repository.DeleteAsync(id);

		public async Task<Todo?> MarkAsCompleteAsync(Guid id)
		{
			var todo = await _repository.GetByIdAsync(id);
			if (todo == null) return null;
			todo.Status = TodoStatus.Completed;
			todo.LastModifiedDate = DateTime.UtcNow;
			return await _repository.UpdateAsync(todo);
		}

	}
}
