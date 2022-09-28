namespace RiotNET.Services.Dragon;

using Microsoft.Extensions.Options;

/// <summary>
/// Provides an implementation for accessing data from Data Dragon.
/// Reference: https://developer.riotgames.com/docs/lol#data-dragon
/// </summary>
public class DataDragon : IDataDragon
{
    private readonly DataDragonOptions config;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataDragon"/> class.
    /// </summary>
    /// <param name="options">The configuration config for the data dragon service.</param>
    public DataDragon(IOptions<DataDragonOptions> options)
    {
        config = options.Value;
    }
}
