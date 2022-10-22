namespace RiotDotNET.Enums;

public enum ChallengeTracking
{
    /// <summary>
    /// stats are incremented without reset
    /// </summary>
    LIFETIME,

    /// <summary>
    /// stats are accumulated by season and reset at the beginning of new season
    /// </summary>
    SEASON
}
