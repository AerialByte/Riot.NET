namespace RiotDotNET.Endpoints.RiotGames.Riot;
using RiotDotNET.Constants;

/// <summary>
/// Represents the riot games Account v1 endpoint.
/// API Documentation: https://developer.riotgames.com/apis#account-v1
/// </summary>
public interface IAccountEndpoint
{
    /// <summary>
    /// Gets the global account information by the player's universal unique id (puuid).
    /// </summary>
    /// <param name="puuid">The player's universal unique id (puuid).</param>
    /// <param name="region">The region to execute the request on.</param>
    /// <returns>The request.</returns>
    EndpointRequest<AccountDto> ByPuuid(string puuid, Region region);

    /// <summary>
    /// Gets the global account information by the player's universal unique id (puuid).
    /// </summary>
    /// <param name="gameName">The player's game name.</param>
    /// <param name="tagLine">The player's tag line (defaults to platform, eg; NA1).</param>
    /// <param name="region">The region to execute the request on.</param>
    /// <returns>The request.</returns>
    EndpointRequest<AccountDto> ByRiotId(string gameName, string tagLine, Region region);
}
