namespace RiotDotNET.Constants;

/// <summary>
/// Defines the host values for various API/CDN endpoints.
/// </summary>
internal static class Default
{
    internal static class Host
    {
        /// <summary>
        /// Community Dragon base host.
        /// </summary>
        public const string CommunityDragonRaw = "raw.communitydragon.org";

        /// <summary>
        /// Data Dragon base host.
        /// </summary>
        public const string DataDragon = "ddragon.leagueoflegends.com";

        /// <summary>
        /// Riot API base host, before being prefixed by a Platform or Platform.
        /// </summary>
        public const string RiotGamesApi = "api.riotgames.com";
    }

    internal static class RiotApi
    {
        /// <summary>
        /// The hader key for the riot token.
        /// </summary>
        public const string TokenHeader = "X-Riot-Token";

        /// <summary>
        /// Paths for all Riot Api endpoints.
        /// </summary>
        internal static class Path
        {
            public const string Account_v1 = "riot/account/v1";
            public const string ChampionMastery_v4 = "lol/champion-mastery/v4";
            public const string Champion_v3 = "lol/platform/v3";
        }
    }
}
