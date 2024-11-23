using Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

public class TodoService : ITodoService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public TodoService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task<List<Todo>> GetAll()
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
        
        return await dbContext.Todos.ToListAsync();
    }

    public async Task<Todo> Create(Todo todo)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
        
        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync();
        
        return todo;
    }
    
    public async Task<Todo?> Update(int id, Todo todo)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
        
        var existingTodo = await dbContext.Todos.FindAsync(id);
        
        if (existingTodo == null)
            return null;
            
        existingTodo.Name = todo.Name;
        existingTodo.IsComplete = todo.IsComplete;
        
        await dbContext.SaveChangesAsync();
        
        return existingTodo;
    }

    public async Task<bool> Delete(int id)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
        
        var todo = await dbContext.Todos.FindAsync(id);
        
        if (todo == null)
            return false;
            
        dbContext.Todos.Remove(todo);
        await dbContext.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<Todo?> Show(int id)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
            
            return await dbContext.Todos.FindAsync(id);
        }
        catch (Exception)
        {
            throw;
        }
    }
    
}