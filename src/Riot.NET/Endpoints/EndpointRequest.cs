namespace RiotNET.Endpoints;
using RiotNET.Extensions;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

/// <summary>
/// Defines an endpoint that's ready to process a request.
/// </summary>
public class EndpointRequest<TResponse>
    where TResponse : class
{
    private static readonly HttpClient httpClient = new(new SocketsHttpHandler() { PooledConnectionLifetime = TimeSpan.FromMinutes(15) });

    private readonly Uri uri;
    private readonly ReadOnlyDictionary<string, string>? headers;

    /// <summary>
    /// Creates a new request from the specified uri and headers.
    /// </summary>
    /// <param name="uri">The request uri.</param>
    /// <param name="headers">The request headers to use.</param>
    internal EndpointRequest(Uri uri, Dictionary<string, string>? headers = null)
    {
        this.uri = uri;
        this.headers = headers?.ReadOnly();
    }

    /// <summary>
    /// Executes the request and returns the response object.
    /// </summary>
    /// <param name="token">The token for cancelling the request.</param>
    /// <returns>The response object, or null if none was specified.</returns>
    public async Task<EndpointResponse<TResponse>> GetReponseAsync(CancellationToken token = default)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            headers?.ForEach(request.Headers.Add);

            var response = await httpClient.SendAsync(request, token);
            if (response.IsSuccessStatusCode)
            {
                var obj = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: token);
                if (obj == null)
                {
                    return new(new Exception($"{GetType().Name}.{nameof(GetReponseAsync)} failed to parse response json into type '{typeof(TResponse).Name}'"));
                }
                else
                {
                    return new(obj);
                }
            }
            else
            {
                return new(new Exception("Unhandled response code."), response.StatusCode);
            }
        }
        catch (HttpRequestException httpEx)
        {
            return new(httpEx, httpEx.StatusCode);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}
