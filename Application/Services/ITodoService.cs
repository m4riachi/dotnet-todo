namespace Application.Services;

public interface ITodoService
{
    Task<List<Todo>> GetAll();
    Task<Todo> Create(Todo todo);
    Task<Todo?> Update(int id, Todo todo);
    Task<bool> Delete(int id);
    Task<Todo?> Show(int id);
}