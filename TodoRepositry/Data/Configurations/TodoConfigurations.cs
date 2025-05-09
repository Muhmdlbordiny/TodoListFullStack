using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoCore.Entities;

namespace TodoRepositry.Data.Configurations
{
	public class TodoConfigurations : IEntityTypeConfiguration<Todo>
	{
		public void Configure(EntityTypeBuilder<Todo> builder)
		{
			builder.Property(t => t.Title)
								.IsRequired()
								.HasMaxLength(100);

			builder.Property(t => t.Status)
				.IsRequired();

			builder.Property(t => t.Priority)
				.IsRequired();

			builder.Property(t => t.CreatedDate)
				.IsRequired();

			
		}
	}
}
