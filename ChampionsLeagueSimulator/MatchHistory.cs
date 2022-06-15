namespace ChampionsLeagueSimulator
{
    public class MatchHistory
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public int HomeTeamPoint => HomeTeamScore - AwayTeamScore > 1 ? 3 : (HomeTeamScore - AwayTeamScore == 0 ? 1 : 0);
        public int AwayTeamPoint => AwayTeamScore - HomeTeamScore > 1 ? 3 : (AwayTeamScore - HomeTeamScore == 0 ? 1 : 0);

    }
}
