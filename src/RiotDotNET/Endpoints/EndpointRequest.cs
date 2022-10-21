namespace RiotDotNET.Endpoints;
using RiotDotNET.Extensions;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

/// <summary>
/// Defines an endpoint that's ready to process a request.
/// </summary>
public class EndpointRequest<TResponse>
    where TResponse : class
{
    private readonly IHttpClientFactory httpClientFactory;

    /// <summary>
    /// Creates a new request from the specified uri and headers.
    /// </summary>
    /// <param name="httpClientFactory">The factory for creating http clients.</param>
    /// <param name="uri">The request uri.</param>
    /// <param name="headers">The request headers to use.</param>
    internal EndpointRequest(IHttpClientFactory httpClientFactory, Uri uri, Dictionary<string, string>? headers = null)
    {
        this.httpClientFactory = httpClientFactory;
        Uri = uri;
        Headers = headers?.ReadOnly();
    }

    /// <summary>
    /// The request Uri.
    /// </summary>
    public Uri Uri { get; init; }

    /// <summary>
    /// The request headers.
    /// </summary>
    public ReadOnlyDictionary<string, string>? Headers { get; init; }

    /// <summary>
    /// Executes the request and returns the response object.
    /// </summary>
    /// <param name="token">The token for cancelling the request.</param>
    /// <returns>The response object, or null if none was specified.</returns>
    public async Task<EndpointResponse<TResponse>> GetReponseAsync(CancellationToken token = default)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, Uri);
            Headers?.ForEach(request.Headers.Add);

            var httpClient = httpClientFactory.CreateClient();
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
                    return new(obj, response.StatusCode);
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
