namespace RiotNET.Interfaces;

/// <summary>
/// Defines an object that contains a host property.
/// </summary>
public interface IHost
{
    /// <summary>
    /// The host section of an endpoint.
    /// </summary>
    string Host { get; }
}
