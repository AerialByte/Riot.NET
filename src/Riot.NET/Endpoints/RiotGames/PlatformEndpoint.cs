namespace RiotNET.Endpoints.RiotGames;

using Microsoft.Extensions.Options;
using RiotNET.Constants;
using RiotNET.Endpoints.RiotGames.Riot;
using RiotNET.Extensions;
using RiotNET.Services.Riot;

internal abstract class PlatformEndpoint : RiotGamesEndpoint
{
    /// <inheritdoc/>
    protected PlatformEndpoint(IOptions<RiotApiOptions> options)
        : base(options)
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
    protected EndpointRequest<AccountDto> Request<TResponse>(Platform platform, string path) => SetPlatform(platform).Request<TResponse>(path);
}
