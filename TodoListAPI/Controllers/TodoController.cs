using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoCore.Dtos;
using TodoCore.Entities;
using TodoCore.ServiceContract;

namespace TodoListAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoController : ControllerBase
	{
		private readonly ITodoService _service;
		private readonly IMapper _mapper;
		public TodoController(ITodoService service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		[HttpGet("getstatus")]
		public async Task<IActionResult> Get([FromQuery] TodoStatus? status, TodoPriority? priority)
		{
			var todos = await _service.GetTodosAsync(status, priority);
			var map = _mapper.Map<IEnumerable<TodoDto>>(todos);
			return Ok(map);
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var todo = await _service.GetTodoByIdAsync(id);
			return todo == null ? NotFound() : Ok(todo);
		}

		[HttpPost("createtodo")]
		public async Task<IActionResult> Create([FromBody] TodoDto dto)
		{
			if (string.IsNullOrWhiteSpace(dto.Title) || dto.Title.Length > 100)
				return BadRequest("Title is required and must be less than 100 characters.");
			var todo = _mapper.Map<Todo>(dto);
			var created = await _service.CreateTodoAsync(todo);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<TodoDto>(created));
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] TodoDto dto)
		{
			if (id != dto.Id) return BadRequest("ID mismatch.");
			var todo = _mapper.Map<TodoDto,Todo>(dto);

			var updated = await _service.UpdateTodoAsync(todo);
			//var map = _mapper.Map<TodoDto>(updated);
			return updated == null ? NotFound() : Ok(updated);
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var success = await _service.DeleteTodoAsync(id);
			return success ? NoContent() : NotFound();
		}

		[HttpPatch("{id}/complete")]
		public async Task<IActionResult> MarkAsComplete(Guid id)
		{
			var updated = await _service.MarkAsCompleteAsync(id);
			var map = _mapper.Map<TodoDto>(updated);
			return updated == null ? NotFound() : Ok(map);
		}
	}


}
