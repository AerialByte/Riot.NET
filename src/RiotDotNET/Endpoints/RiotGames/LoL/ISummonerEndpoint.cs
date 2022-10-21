namespace RiotDotNET.Endpoints.RiotGames.LoL;
using RiotDotNET.Constants;

/// <summary>
/// Represents the riot games summoner v4 endpoint.
/// API Documentation: https://developer.riotgames.com/apis#summoner-v4
/// </summary>
public interface ISummonerEndpoint
{
    /// <summary>
    /// Gets the platform account information by the player's platform-specific summoner id.
    /// </summary>
    /// <param name="summonerId">The player's platform-specific summoner id.</param>
    /// <param name="platform">The platform to execute the request on.</param>
    /// <returns>The request.</returns>
    EndpointRequest<SummonerDto> ById(string summonerId, Platform platform);

    /// <summary>
    /// Gets the platform account information by the player's account id.
    /// </summary>
    /// <param name="accountId">The player's account id.</param>
    /// <param name="platform">The platform to execute the request on.</param>
    /// <returns>The request.</returns>
    EndpointRequest<SummonerDto> ByAccount(string accountId, Platform platform);

    /// <summary>
    /// Gets the platform account information by the player's game name.
    /// </summary>
    /// <param name="name">The player's game name.</param>
    /// <param name="platform">The platform to execute the request on.</param>
    /// <returns>The request.</returns>
    EndpointRequest<SummonerDto> ByName(string name, Platform platform);

    /// <summary>
    /// Gets the platform account information by the player's universal unique id (puuid).
    /// </summary>
    /// <param name="puuid">The player's universal unique id (puuid).</param>
    /// <param name="platform">The platform to execute the request on.</param>
    /// <returns>The request.</returns>
    EndpointRequest<SummonerDto> ByPuuid(string puuid, Platform platform);
}
