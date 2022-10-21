namespace RiotDotNETTests.TestClient;
using System.Net.Http;

internal class TestHttpClientFactory : IHttpClientFactory
{
    public static TestHttpClientFactory Default { get; } = new();

    public HttpClient CreateClient(string name) => new(new TestHttpMessageHandler());
}
