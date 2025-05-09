using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TodoCore.Entities
{
	public class Todo
	{
		public Guid Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string? Description { get; set; }

		public TodoStatus Status { get; set; } = TodoStatus.Pending;

		public TodoPriority Priority { get; set; } = TodoPriority.Medium;

		public DateTime? DueDate { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

		public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

	}
}
