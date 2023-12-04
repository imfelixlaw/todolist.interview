using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Dtos;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers;

[ApiController, Route("api/[controller]")]
[Authorize]
public class TodoController(ILogger<TodoController> logger,
    ITodoService todoService) : ControllerBase
{
    private readonly ILogger<TodoController> _logger = logger;
    private readonly ITodoService _todoService = todoService;

    // GET: api/<TodoController>
    [HttpGet]
    public async Task<ActionResult<IList<TodoChildResponseDto>>> Get([FromQuery] QueryParameter? param)
    {
        var result = await _todoService.GetTodoListAsync(param);

        return Ok(result);
    }

    // GET api/<TodoController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoChildResponseDto>> Get(Guid id)
    {
        var result = await _todoService.GetTodoAsync(id);

        return Ok(result);
    }

    // POST api/<TodoController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TodoChildDto value)
    {
        try
        {
            ApplicationUser user = new ApplicationUser();
            user.Email = Request.Headers["user"];

            var result = await _todoService.AddTodoAsync(value, user);
        }
        catch (Exception ex)
        {

        }
        return Created();
    }

    // PUT api/<TodoController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] TodoChildDto value)
    {
        ApplicationUser user = new ApplicationUser();
        user.Email = Request.Headers["user"];

        await _todoService.UpdateTodoAsync(id, value, user);

        return Ok();
    }

    // DELETE api/<TodoController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        ApplicationUser user = new ApplicationUser();
        user.Email = Request.Headers["user"];

        await _todoService.DeleteTodoAsync(id, user);
        return Ok();
    }
}
