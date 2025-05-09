using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoCore.Dtos;
using TodoCore.Entities;

namespace TodoCore.ProfileMap
{
	public class TodoMappingProfile:Profile
	{
		public TodoMappingProfile()
		{
			CreateMap<Todo,TodoDto>().ReverseMap();
		}
	}
}
