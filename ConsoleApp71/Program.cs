using Microsoft.Data.SqlClient;
using Turnir.DAL;

namespace ConsoleApp71
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ClubContext())
            {
                var teams = context.Club.ToList();
                foreach (var team in teams)
                {
                    Console.WriteLine($"{team.NameTeam}, {team.TownTeam}, {team.CountWin}, {team.CountDefeats}, {team.CountGames}");
                }
            }
        }

    }
}