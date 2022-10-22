namespace RiotDotNET.Enums;

public enum ChallengeState
{
    /// <summary>
    /// not visible and not calculated
    /// </summary>
    DISABLED,

    /// <summary>
    /// visible and calculated
    /// </summary>
    ENABLED,

    /// <summary>
    /// not visible, but calculated
    /// </summary>
    HIDDEN,

    /// <summary>
    /// visible, but not calculated
    /// </summary>
    ARCHIVED
}
