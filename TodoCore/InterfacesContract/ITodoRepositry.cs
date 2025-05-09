using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoCore.Entities;

namespace TodoCore.InterfacesContract
{
	public interface ITodoRepositry
	{
		Task<IEnumerable<Todo>> GetAllAsync(TodoStatus? status, TodoPriority? priority);
		Task<Todo?> GetByIdAsync(Guid id);
		Task<Todo> AddAsync(Todo todo);
		Task<Todo?> UpdateAsync(Todo todo);
		Task<bool> DeleteAsync(Guid id);

	}
}
