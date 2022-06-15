using System.Collections.Generic;

namespace ChampionsLeagueSimulator
{
    public class TeamGroup
    {
        public List<Team> Teams { get; set; } 
        public List<MatchHistory> MatchHistories { get; set; }
        public string Name { get; set; }
        public TeamGroup(string name)
        {
            Teams = new List<Team>();
            Name = name;
            MatchHistories = new List<MatchHistory>();
        }

    }
}
