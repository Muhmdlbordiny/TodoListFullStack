using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoCore.Dtos;
using TodoCore.Entities;

namespace TodoCore.ServiceContract
{
	public interface ITodoService
	{
		Task<IEnumerable<Todo>> GetTodosAsync(TodoStatus? status, TodoPriority? priority);
		Task<Todo?> GetTodoByIdAsync(Guid id);
		Task<Todo> CreateTodoAsync(Todo todo);
		Task<Todo?> UpdateTodoAsync(Todo todo);
		Task<bool> DeleteTodoAsync(Guid id);
		Task<Todo?> MarkAsCompleteAsync(Guid id);

	}
}
