var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var todos = new List<object>();

app.MapGet("/todos", () => todos);

app.Run();

public partial class Program;
