using RiotNET.Endpoints.RiotGames.Riot;

namespace RiotNET.Services.Riot;

/// <summary>
/// Represents the riot api service.
/// </summary>
public interface IRiotApi
{
    /// <inheritdoc cref="IAccountEndpoint"/>
    IAccountEndpoint Account { get; }
}
