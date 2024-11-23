using Application.Extensions;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configure endpoints
app.MapGet("/todos", async (ITodoService todoService) =>
{
    var todos = await todoService.GetAll();
    return Results.Ok(todos);
});

app.MapPost("/todos", async (ITodoService todoService, Todo todo) =>
{
    var newTodo = await todoService.Create(todo);
    return Results.Created($"/todos/{newTodo.Id}", newTodo);
});

app.MapPut("/todos/{id}", async (ITodoService todoService, int id, Todo todo) =>
{
    var updatedTodo = await todoService.Update(id, todo);
    return updatedTodo is not null ? Results.Ok(updatedTodo) : Results.NotFound();
});

app.MapDelete("/todos/{id}", async (ITodoService todoService, int id) =>
{
    var deleted = await todoService.Delete(id);
    return deleted ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/todos/{id}", async (ITodoService todoService, int id) =>
{
    var todo = await todoService.Show(id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});
app.Run();
