var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var todos = new List<Todo>();
var nextId = 1;

app.MapGet("/todos", () => todos);

app.MapPost("/todos", (TodoCreate request) =>
{
    if (string.IsNullOrWhiteSpace(request.Title))
    {
        return Results.BadRequest();
    }

    var todo = new Todo(nextId++, request.Title, false);
    todos.Add(todo);
    return Results.Json(todo, statusCode: StatusCodes.Status201Created);
});

app.Run();

public partial class Program;

record Todo(int Id, string Title, bool IsComplete);

record TodoCreate(string? Title);
