namespace RiotDotNET.Endpoints.RiotGames.Riot;
using Microsoft.Extensions.Options;
using RiotDotNET.Constants;
using RiotDotNET.Services.Riot;

/// <inheritdoc cref="IAccountEndpoint"/>
internal sealed class AccountEndpoint : RegionEndpoint, IAccountEndpoint
{
    /// <inheritdoc cref="RegionEndpoint(IHttpClientFactory, IOptions{RiotApiOptions})"/>
    internal AccountEndpoint(IHttpClientFactory httpClientFactory, IOptions<RiotApiOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <inheritdoc/>
    public EndpointRequest<AccountDto> ByPuuid(string puuid, Region region) =>
        Request<AccountDto>(region, $"{Default.RiotApi.Path.Account_v1}/accounts/by-puuid/{puuid}");

    public EndpointRequest<AccountDto> ByRiotId(string gameName, string tagLine, Region region) =>
        Request<AccountDto>(region, $"{Default.RiotApi.Path.Account_v1}/accounts/by-riot-id/{gameName}/{tagLine}");
}
