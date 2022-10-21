namespace RiotDotNET.Endpoints.RiotGames;

using Microsoft.Extensions.Options;
using RiotDotNET.Constants;
using RiotDotNET.Endpoints.RiotGames.Riot;
using RiotDotNET.Extensions;
using RiotDotNET.Services.Riot;

internal abstract class PlatformEndpoint : RiotGamesEndpoint
{
    /// <inheritdoc/>
    protected PlatformEndpoint(IHttpClientFactory httpClientFactory, IOptions<RiotApiOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// The endpoint platform for building the URI.
    /// </summary>
    public virtual Platform? Platform { get; protected set; }

    /// <inheritdoc/>
    protected override string Route => Platform?.Route.ToStringLower() ?? throw new Exception("Platform must be defined.");

    /// <summary>
    /// Sets the platform to the one specified and returns the endpoint.
    /// </summary>
    /// <param name="region">The request platform.</param>
    /// <returns>The endpoint with the updated platform.</returns>
    protected PlatformEndpoint SetPlatform(Platform platform)
    {
        Platform = platform;
        return this;
    }

    /// <inheritdoc cref="EndpointBase.Request{TResponse}(string)"/>
    /// <param name="region">The request platform.</param>
    protected EndpointRequest<TResponse> Request<TResponse>(Platform platform, string path) where TResponse : class =>
        SetPlatform(platform).Request<TResponse>(path);
}
