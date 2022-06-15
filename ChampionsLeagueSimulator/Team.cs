using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeagueSimulator
{
    public class Team
    {
        public Team(string name, string country, int bag)
        {
            Name = name;
            Country = country;
            Bag = bag;
        }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Bag { get; set; }
        public bool IsFirstMatch { get; set; } // İlk eşleşme aşamasında eşlendi mi?
        public int Point { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalsAverage => GoalsFor - GoalsAgainst;

    }
}
