using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoApi.Tests;

public class PostTodosEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PostTodosEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostTodos_WithValidTitle_Returns201AndCreatedTodo()
    {
        var response = await _client.PostAsJsonAsync("/todos", new { title = "Buy milk" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var todo = document.RootElement;

        Assert.Equal(JsonValueKind.Number, todo.GetProperty("id").ValueKind);
        Assert.Equal("Buy milk", todo.GetProperty("title").GetString());
        Assert.False(todo.GetProperty("isComplete").GetBoolean());
    }

    [Fact]
    public async Task PostTodos_WithEmptyTitle_Returns400()
    {
        var response = await _client.PostAsJsonAsync("/todos", new { title = "" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostTodos_ThenGetTodos_IncludesCreatedItem()
    {
        const string title = "Walk dog";

        var post = await _client.PostAsJsonAsync("/todos", new { title });
        Assert.Equal(HttpStatusCode.Created, post.StatusCode);

        var get = await _client.GetAsync("/todos");
        using var document = JsonDocument.Parse(await get.Content.ReadAsStringAsync());

        Assert.Contains(
            document.RootElement.EnumerateArray(),
            item => item.GetProperty("title").GetString() == title);
    }
}
