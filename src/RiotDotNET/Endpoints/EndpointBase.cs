namespace RiotDotNET.Endpoints;
using RiotDotNET.Endpoints.RiotGames.Riot;
using RiotDotNET.Extensions;
using System.Collections.ObjectModel;

/// <summary>
/// Represents an endpoint to either an API
/// </summary>
public abstract class EndpointBase
{
    private readonly IHttpClientFactory httpClientFactory;

    /// <summary>
    /// The base endpoint class.
    /// </summary>
    /// <param name="httpClientFactory">The factory for creating the requests.</param>
    protected EndpointBase(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// The uri host.
    /// </summary>
    protected abstract string Host { get; }

    /// <summary>
    /// The uri scheme (defaults to https).
    /// </summary>
    protected virtual string Scheme { get; } = Uri.UriSchemeHttps;

    /// <summary>
    /// The request headers to send.
    /// </summary>
    protected Dictionary<string, string> Headers { get; } = new();

    /// <summary>
    /// The uri to connect to this endpoint.
    /// </summary>
    public virtual Uri CreateUri(string path) => new UriBuilder(Scheme, Host) { Path = path }.Uri;

    /// <summary>
    /// Gets the headers for the request.
    /// </summary>
    /// <returns>The headers.</returns>
    public ReadOnlyDictionary<string, string> GetHeaders() => Headers.ReadOnly();

    /// <summary>
    /// Creates the request with the current generated uri.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <returns>A new request instance.</returns>
    protected EndpointRequest<TResponse> Request<TResponse>(string path)
        where TResponse : class =>
        new(httpClientFactory, CreateUri(path), Headers);
}
