using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoCore.Dtos
{
	public class TodoDto
	{
		public Guid Id { get; set; }
		[Required]
		[MaxLength(100)]
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string Status { get; set; } = "Pending";
		public string Priority { get; set; } = "Medium";
		public DateTime? DueDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastModifiedDate { get; set; }

	}
}
