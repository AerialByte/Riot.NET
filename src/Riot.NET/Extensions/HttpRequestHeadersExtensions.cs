namespace RiotNET.Extensions;
using System.Net.Http.Headers;

public static class HttpRequestHeadersExtensions
{
    /// <inheritdoc cref="HttpHeaders.Add"/>
    public static void Add(this HttpRequestHeaders headers, KeyValuePair<string, string> data) => headers.Add(data.Key, data.Value);
}
