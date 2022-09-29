namespace RiotDotNET.Endpoints;
using RiotDotNET.Endpoints.RiotGames.Riot;

/// <summary>
/// Represents an endpoint to either an API
/// </summary>
public abstract class EndpointBase
{
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
    protected virtual Uri CreateUri(string path) => new UriBuilder(Scheme, Host) { Path = path }.Uri;

    /// <summary>
    /// Creates the request with the current generated uri.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <returns>A new request instance.</returns>
    protected EndpointRequest<AccountDto> Request<TResponse>(string path) => new(CreateUri(path), Headers);
}
