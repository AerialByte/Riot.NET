namespace RiotDotNET.Services.Riot;
using RiotDotNET.Endpoints.RiotGames.Riot;

/// <summary>
/// Represents the riot api service.
/// </summary>
public interface IRiotApi
{
    /// <inheritdoc cref="IAccountEndpoint"/>
    IAccountEndpoint Account { get; }
}
