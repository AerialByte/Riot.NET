namespace RiotDotNET.Endpoints.RiotGames.Riot;
using Microsoft.Extensions.Options;
using RiotDotNET.Constants;
using RiotDotNET.Services.Riot;

/// <inheritdoc cref="IAccountEndpoint"/>
internal class AccountEndpoint : RegionEndpoint, IAccountEndpoint
{
    /// <inheritdoc cref="RegionEndpoint(IOptions{RiotApiOptions}, Region)"/>
    public AccountEndpoint(IOptions<RiotApiOptions> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public EndpointRequest<AccountDto> ByPuuid(string puuid, Region region) =>
        Request<AccountDto>(region, $"{Default.RiotApi.Path.Account_v1}/accounts/by-puuid/{puuid}");
}
