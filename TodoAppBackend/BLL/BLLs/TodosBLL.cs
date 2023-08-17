using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.BLLs
{
    public class TodosBLL
    {
        private readonly TodoDbContext _todoDbContext;
        private readonly IMapper _mapper;
        public TodosBLL(TodoDbContext todoDbContext, IMapper mapper) 
        { 
            _todoDbContext = todoDbContext;
            _mapper = mapper;
        }
        public async Task<List<TodoDto>> GetAllTodos(string userId)
        {
            List<Todo> TodosToShow = await _todoDbContext.Todos
                .Where(todo => todo.ApplicationUserId == userId && !todo.IsDeleted)
                .Select(todo => new Todo { Id = todo.Id, Description = todo.Description, CreatedDate = todo.CreatedDate, IsCompleted = todo.IsCompleted })
                .OrderByDescending(todo => todo.CreatedDate)
                .ToListAsync();


            return _mapper.Map<List<TodoDto>>(TodosToShow);
        }

        public async Task AddTodo(TodoDto todoDto, string userId)
        {
            Todo todo = _mapper.Map<Todo>(todoDto);
            todo.Id = Guid.NewGuid();
            todo.ApplicationUserId = userId;

            await _todoDbContext.Todos.AddAsync(todo);
            await _todoDbContext.SaveChangesAsync();
        }

        public async Task<TodoDto?> UpdateAndReturnTodo(Guid id, bool isCompleted)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null)
                return null;

            todo.IsCompleted = isCompleted;
            todo.CompletedDate = DateTime.Now;
            await _todoDbContext.SaveChangesAsync();

            return _mapper.Map<TodoDto>(todo);
        }

        public async Task<TodoDto?> DeleteAndReturnTodo(Guid id)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null)
                return null;

            todo.IsDeleted = true;
            todo.DeletedDate = DateTime.Now;
            await _todoDbContext.SaveChangesAsync();

            return _mapper.Map<TodoDto>(todo);
        }
    }
}
