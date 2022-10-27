namespace RiotDotNET.Services.Riot;

using RiotDotNET.Endpoints.Interfaces;

/// <summary>
/// Represents the riot api service.
/// </summary>
public interface IRiotApi
{
    /// <inheritdoc cref="IAccountEndpoint"/>
    IAccountEndpoint Account { get; }

    /// <inheritdoc cref="ISummonerEndpoint"/>
    ISummonerEndpoint Summoner { get; }
}
