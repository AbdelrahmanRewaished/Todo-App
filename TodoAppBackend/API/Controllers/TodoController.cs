using BusinessLogicLayer.BLLs;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Authorize(Roles = "Client", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TodosBLL _todosBLL;

        private string userEmail;
        private ApplicationUser user;

        public TodoController(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            TodosBLL todosBLL,
            IJwtService jwtService)
        {
            HttpContext httpContext = httpContextAccessor.HttpContext!;
            userEmail = jwtService.GetUserEmail(httpContext)!;
            _userManager = userManager;
            _todosBLL = todosBLL;
        }

        private async Task AssignCurrentUser()
        {
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(userEmail)!;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            await AssignCurrentUser();
            List<TodoDto> todos = await _todosBLL.GetAllTodos(user.Id);
            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(TodoDto todo)
        {
            await AssignCurrentUser();
            await _todosBLL.AddTodo(todo, user.Id);
            return Ok(todo.Id);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, [FromBody]bool isCompleted)
        {
            TodoDto todo = await _todosBLL.UpdateAndReturnTodo(id, isCompleted);
            if(todo == null)
            {
                return NotFound(new {message = "Todo you are trying to update does not exist"});
            }
            return Ok(todo.Id);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            TodoDto todo = await _todosBLL.DeleteAndReturnTodo(id);
            if(todo == null)
            {
                return NotFound(new { message = "Todo you are trying to delete does not exist" });
            }
            return Ok(todo.Id);
        }
    }
}
