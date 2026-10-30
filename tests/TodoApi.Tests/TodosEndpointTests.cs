using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoApi.Tests;

public class TodosEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TodosEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTodos_Returns200()
    {
        var response = await _client.GetAsync("/todos");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetTodos_ReturnsJsonArray()
    {
        var response = await _client.GetAsync("/todos");
        var body = await response.Content.ReadAsStringAsync();

        using var document = JsonDocument.Parse(body);

        Assert.Equal(JsonValueKind.Array, document.RootElement.ValueKind);
    }

    [Fact]
    public async Task GetTodos_WhenStoreIsEmpty_ReturnsEmptyArray()
    {
        var response = await _client.GetAsync("/todos");
        var body = await response.Content.ReadAsStringAsync();

        using var document = JsonDocument.Parse(body);

        Assert.Equal(JsonValueKind.Array, document.RootElement.ValueKind);
        Assert.Equal(0, document.RootElement.GetArrayLength());
    }
}
