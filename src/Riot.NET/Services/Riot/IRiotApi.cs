namespace RiotNET.Services.Riot;
using RiotNET.Endpoints.RiotGames.Riot;

/// <summary>
/// Represents the riot api service.
/// </summary>
public interface IRiotApi
{
    /// <inheritdoc cref="IAccountEndpoint"/>
    IAccountEndpoint Account { get; }
}
