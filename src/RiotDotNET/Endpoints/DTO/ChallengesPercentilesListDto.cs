namespace RiotDotNET.Endpoints.DTO;
using RiotDotNET.Enums;
using System.Collections.Generic;

public class ChallengesPercentilesListDto : Dictionary<long, Dictionary<LeagueRankTier, double>>
{
}
