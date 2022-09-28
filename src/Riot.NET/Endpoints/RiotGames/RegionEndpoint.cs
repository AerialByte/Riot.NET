namespace RiotNET.Endpoints.RiotGames;
using Microsoft.Extensions.Options;
using RiotNET.Constants;
using RiotNET.Endpoints.RiotGames.Riot;
using RiotNET.Extensions;
using RiotNET.Services.Riot;

internal abstract class RegionEndpoint : RiotGamesEndpoint
{
    /// <inheritdoc/>
    protected RegionEndpoint(IOptions<RiotApiOptions> options)
        : base(options)
    {
    }

    /// <summary>
    /// The endpoint region for building the URI.
    /// </summary>
    public virtual Region? Region { get; protected set; }

    /// <inheritdoc/>
    protected override string Route => Region?.Route.ToStringLower() ?? throw new Exception("Region must be defined.");

    /// <summary>
    /// Sets the region to the one specified and returns the endpoint.
    /// </summary>
    /// <param name="region">The request region.</param>
    /// <returns>The endpoint with the updated region.</returns>
    protected RegionEndpoint SetRegion(Region region)
    {
        Region = region;
        return this;
    }

    /// <inheritdoc cref="EndpointBase.Request{TResponse}(string)"/>
    /// <param name="region">The request region.</param>
    protected EndpointRequest<AccountDto> Request<TResponse>(Region region, string path) => SetRegion(region).Request<TResponse>(path);
}