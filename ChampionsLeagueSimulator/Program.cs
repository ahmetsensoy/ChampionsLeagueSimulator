using System;
using System.Collections.Generic;
using System.Linq;

namespace ChampionsLeagueSimulator
{
    class Program
    {
        public static string Separator => "----------------------";
        static void Main(string[] args)
        {
            var teamGroups = new List<TeamGroup>();
            var bags = new Dictionary<int, List<Team>>();
            var last16 = new List<Team>();
            InitializeBags(bags);
            Console.WriteLine("Please press any key for first matches..");
            Console.ReadKey();
            Console.WriteLine();
            FirstMatches(bags, teamGroups);

            Console.WriteLine("Please press any key for group matches..");
            Console.ReadKey();
            Console.WriteLine();

            GroupMatches(teamGroups, last16);

            Console.WriteLine(Separator);
            Console.WriteLine("Last 16 Teams:");
            last16.ForEach(x => Console.WriteLine($"[{x.Name}] - {x.Country}"));
        }

        private static void InitializeBags(Dictionary<int, List<Team>> bags)
        {

            var bag_1 = new List<Team>() {
             new Team("Bayern Munich", CountryConstants.Germany, 1),
             new Team("Sevilla", CountryConstants.Spain, 1),
             new Team("Real Madrid", CountryConstants.Spain, 1),
             new Team("Liverpool", CountryConstants.England, 1),
             new Team("Juventus", CountryConstants.Italy, 1),
             new Team("Paris Saint-Germain", CountryConstants.France, 1),
             new Team("Zenit", CountryConstants.Russia, 1),
             new Team("Porto", CountryConstants.Portugal, 1),
            };
            bags.Add(1, bag_1);

            var bag_2 = new List<Team>() {
             new Team("Barcelona", CountryConstants.Spain,2),
             new Team("Atletico Madrid", CountryConstants.Spain,2),
             new Team("Manchester City", CountryConstants.England,2),
             new Team("Manchester United", CountryConstants.England,2),
             new Team("Borussia Dortmund", CountryConstants.Germany,2),
             new Team("Shacktar Doetsk", CountryConstants.Ukraine,2),
             new Team("Chelsea", CountryConstants.England,2),
             new Team("Ajax", CountryConstants.Netherlands,2),
            };
            bags.Add(2, bag_2);

            var bag_3 = new List<Team>() {
             new Team("Dynamo Kiev", CountryConstants.Ukraine,3),
             new Team("Red Bull Salzburg", CountryConstants.Germany,3),
             new Team("RP Leipzig", CountryConstants.Germany,3),
             new Team("Internazionale", CountryConstants.Italy,3),
             new Team("Olypiacos", CountryConstants.Greece,3),
             new Team("Lazio", CountryConstants.Italy,3),
             new Team("Krasnodar", CountryConstants.Russia,3),
             new Team("Atalanta", CountryConstants.Italy,3),
            };
            bags.Add(3, bag_3);

            var bag_4 = new List<Team>() {
             new Team("Lokomotiv Moskova", CountryConstants.Russia,4),
             new Team("Marseille", CountryConstants.France,4),
             new Team("Club Brugge", CountryConstants.Belgium,4),
             new Team("Bor. Mönchengladbach", CountryConstants.Germany,4),
             new Team("Galatasaray", CountryConstants.Türkiye,4),
             new Team("Midtjlland", CountryConstants.Denmark,4),
             new Team("Rennes", CountryConstants.France,4),
             new Team("Ferencvaros", CountryConstants.Hungary,4),
            };
            bags.Add(4, bag_4);

            Console.WriteLine("---Bag 1---");
            bag_1.ForEach(x => Console.WriteLine($"[{x.Name}] - {x.Country}"));
            Console.WriteLine();

            Console.WriteLine("---Bag 2---");
            bag_2.ForEach(x => Console.WriteLine($"[{x.Name}] - {x.Country}"));
            Console.WriteLine();

            Console.WriteLine("---Bag 3---");
            bag_3.ForEach(x => Console.WriteLine($"[{x.Name}] - {x.Country}"));
            Console.WriteLine();

            Console.WriteLine("---Bag 4---");
            bag_4.ForEach(x => Console.WriteLine($"[{x.Name}] - {x.Country}"));
            Console.WriteLine();
        }

        private static void FirstMatches(Dictionary<int, List<Team>> bags, List<TeamGroup> teamGroups)
        {
            foreach (var bag in bags)
            {
                var teamNumberIndex = 1;
                foreach (var bagTeam in bag.Value)
                {
                    if (bagTeam.IsFirstMatch) continue;
                    var teamGroup = new TeamGroup($"Team-{teamNumberIndex}");

                    Console.WriteLine(Separator);
                    Console.WriteLine(teamGroup.Name);
                    teamNumberIndex++;
                    teamGroup.Teams.Add(bagTeam);

                    bagTeam.IsFirstMatch = true;
                    foreach (var otherBag in bags.Where(x => x.Key != bag.Key))
                    {
                        var teamCountries = teamGroup.Teams.Select(x => x.Country).Distinct();

                        var otherBagTeams = otherBag.Value.Where(x => !x.IsFirstMatch && !teamCountries.Contains(x.Country));
                        Random random = new Random();
                        int index = random.Next(0, otherBagTeams.Count() - 1);
                        var matchedOtherTeam = otherBagTeams.ElementAt(index);
                        teamGroup.Teams.Add(matchedOtherTeam);

                        matchedOtherTeam.IsFirstMatch = true;
                    }
                    teamGroup.Teams.ForEach(x => Console.WriteLine($"[{x.Name}] - {x.Country} - (Bag:{x.Bag})"));
                    teamGroups.Add(teamGroup);

                }
                Console.WriteLine();
            }
        }

        private static void GroupMatches(List<TeamGroup> teamGroups, List<Team> last16)
        {
            foreach (var teamGroup in teamGroups)
            {
                foreach (var homeTeam in teamGroup.Teams)
                {

                    foreach (var awayTeam in teamGroup.Teams.Where(x => x != homeTeam))
                    {

                        MatchHistory matchHistory = new MatchHistory();
                        matchHistory.HomeTeam = homeTeam;
                        matchHistory.AwayTeam = awayTeam;

                        Random random = new Random();
                        matchHistory.HomeTeamScore = random.Next(0, 8);
                        matchHistory.AwayTeamScore = random.Next(0, 8);

                        awayTeam.Point += matchHistory.AwayTeamPoint;
                        awayTeam.GoalsAgainst += matchHistory.HomeTeamScore;
                        awayTeam.GoalsFor += matchHistory.AwayTeamScore;

                        homeTeam.Point += matchHistory.HomeTeamPoint;
                        homeTeam.GoalsAgainst += matchHistory.AwayTeamScore;
                        homeTeam.GoalsFor += matchHistory.HomeTeamScore;

                        teamGroup.MatchHistories.Add(matchHistory);
                    }
                }
                Console.WriteLine(Separator);
                Console.WriteLine(teamGroup.Name);
                teamGroup.Teams.OrderByDescending(x => x.Point).ThenByDescending(x => x.GoalsAverage).ToList().ForEach(x => Console.WriteLine($"[{x.Name}] - Point: {x.Point} - Average: {x.GoalsAverage}"));
                Console.WriteLine();

                teamGroup.Teams.OrderByDescending(x => x.Point).ThenByDescending(x => x.GoalsAverage).Take(2).ToList().ForEach(x => last16.Add(x));
            }
        }
    }
}
