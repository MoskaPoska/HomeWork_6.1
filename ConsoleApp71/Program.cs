using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Net.Http.Headers;
using System.Transactions;
using Turnir.DAL;
using Turnir.DAL.Model;

namespace ConsoleApp71
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            using (var context = new ClubContext())
            {               
                if(context.Club.Count()==0)
                {
                    Add();
                }
                var teams = context.Club.ToList();
                foreach(var team in teams)
                {
                    Console.WriteLine($"{team.NameTeam}, {team.TownTeam}, {team.CountWin}, {team.CountDefeats}, {team.CountGames}, {team.CountGoalSC}, {team.CountGoalCo}");
                    foreach(var teamR in teams)
                    {
                        //3.2 
                        if (teamR.CountWin == 30)
                        {
                            teamR.CountWin=35;
                            Update(teamR);
                        }
                        
                    }
                    if (team.NameTeam == "Shahter")
                    {
                        Remove(team);
                    }
                }
                context.SaveChanges();
                //var teams = context.Club.ToList();
                //foreach (var team in teams)
                //{
                //    Console.WriteLine($"{team.NameTeam}, {team.TownTeam}, {team.CountWin}, {team.CountDefeats}, {team.CountGames}");
                //}

                while (true)
                {
                    Console.WriteLine("1. Пошук інформації за назвою команди");
                    Console.WriteLine("2. Пошук статистики ігор за назвою міста команди");
                    Console.WriteLine("3. Пошук інформації за кількістю перемог");
                    Console.WriteLine("4. Пошук ігор за кількістю поразок");
                    Console.WriteLine("5. Пошук ігор за кількістю голів");
                    Console.WriteLine("6. Відображення інформації про всі команди в одному місті");
                    Console.WriteLine("7. Відображення інформації про всі команди в різних містах");
                    Console.WriteLine("8. Показати команду з максимальною кількістю проданих квитків");
                    Console.WriteLine("9. Показати команду з мінімальною кількістю проданих квитків");
                    Console.WriteLine("10. Відобразити топ-3 найпопулярніших конанди по кількістю квитків");
                    Console.WriteLine("11. Відобразити топ-3 найнепопулярніших конанди по кількістю квитків");
                    //Console.WriteLine("12. Додавання нової команди");
                    string choice= Console.ReadLine();
                    switch(choice)
                    {
                        case "1":
                            FindInfoNameTeam();
                            break;
                        case "2":
                            FindStatisticGames();
                            break;
                        case "3":
                            FindInfoFromCountWin();
                            break;
                        case "4":
                            FindInfoFromCounDefeat();
                            break;
                        case "5":
                            FindInfoFromCountGoal();
                            break;
                        case "6":
                            DisplayInfoOneTeam();
                            break;
                        case "7":
                            DisplayInfoOtherTeam();
                            break;
                        case "8":
                            DisplayInfoMaxSoldTickets();
                            break;
                        case "9":
                            DisplayInfoMinSoldTickets();
                            break;
                        case "10":
                            DisplayTop3HighTeams();
                            break;
                        case "11":
                            DisplayTop3LowTeams();
                            break;
                        //case "12":
                        //    Add();
                        //    break;
                        default:                           
                            break;
                    }
                }
            }
        }
        static void DisplayTop3LowTeams()
        {
            using (var context = new ClubContext())
            {
                var teams = context.Club.GroupBy(t => t.NameTeam).Select(t => new { NameTeam = t.Key, TicketsSold = t.Sum(t => t.TicketsSold) }).OrderBy(t => t.NameTeam).Take(3).ToList();
                foreach (var team in teams)
                {
                    Console.WriteLine($"{team.NameTeam}, {team.TicketsSold}");
                }
            }
        }
        static void DisplayTop3HighTeams()
        {
            using(var context= new ClubContext())
            {
                var teams = context.Club.GroupBy(t => t.NameTeam).Select(t => new { NameTeam = t.Key, TicketsSold = t.Sum(t => t.TicketsSold) }).OrderByDescending(t => t.NameTeam).Take(3).ToList();
                foreach (var team in teams)
                {
                    Console.WriteLine($"{team.NameTeam}, {team.TicketsSold}");
                }
            }
        }
        static void DisplayInfoMinSoldTickets()
        {
            using (var context = new ClubContext())
            {
                var ticketSoldMax = context.Club.Min(t => (int?)t.TicketsSold);

                if (ticketSoldMax != null)
                {
                    var game = context.Club.FirstOrDefault(t => t.TicketsSold == ticketSoldMax);

                    if (game != null)
                    {
                        Console.WriteLine($"{game.NameTeam}, {game.TownTeam}, {game.CountWin}, {game.CountDefeats}, {game.CountGames}, {game.CountGoalSC}, {game.CountGoalCo}");
                    }
                }
                else
                {
                    Console.WriteLine("Немає даних про продані квитки.");
                }
            }
        }
        static void DisplayInfoMaxSoldTickets()
        {
            using (var context = new ClubContext())
            {
                var ticketSoldMax = context.Club.Max(t => (int?)t.TicketsSold);

                if (ticketSoldMax != null)
                {
                    var game = context.Club.FirstOrDefault(t => t.TicketsSold == ticketSoldMax);

                    if (game != null)
                    {
                        Console.WriteLine($"{game.NameTeam}, {game.TownTeam}, {game.CountWin}, {game.CountDefeats}, {game.CountGames}, {game.CountGoalSC}, {game.CountGoalCo}");
                    }
                }
                else
                {
                    Console.WriteLine("Немає даних про продані квитки.");
                }
            }
        }
        static void DisplayInfoOtherTeam()
        {
            using (var context = new ClubContext())
            {
                var cityName = "Madrid";
                var command = context.Club.Where(t => t.TownTeam != cityName).ToList();
                foreach (var team in command)
                {
                    Console.WriteLine($"{team.NameTeam}, {team.TownTeam}, {team.CountWin}, {team.CountDefeats}, {team.CountGames}, {team.CountGoalSC}, {team.CountGoalCo}");
                }
            }
        }
        static void DisplayInfoOneTeam()
        {
            using (var context = new ClubContext())
            {
                var cityName = "Donestk";
                var teamss = context.Club.Where(t => t.TownTeam == cityName).ToList();
                foreach (var team in teamss)
                {
                    Console.WriteLine($"{team.NameTeam}, {team.TownTeam}, {team.CountWin}, {team.CountDefeats}, {team.CountGames}, {team.CountGoalSC}, {team.CountGoalCo}");
                }
            }
        }
        static void FindInfoNameTeam()
        {
            using (var context = new ClubContext())
            {
                var teamName = "Shahter";
                var team = context.Club.SingleOrDefault(t => t.NameTeam == teamName);
                if(team!=null)
                {
                    Console.WriteLine($"{team.TownTeam}, {team.CountWin}, {team.CountDefeats}, {team.CountGames}, {team.CountGoalSC}, {team.CountGoalCo}");
                }
            }
        }
        static void FindStatisticGames()
        {
            using (var context= new ClubContext())
            {
                var townTeam = "Madrid";
                var town=context.Club.SingleOrDefault(t=>t.TownTeam == townTeam); 
                if(town!=null)
                {
                    Console.WriteLine($"{town.CountWin}, {town.CountDefeats}, {town.CountGames}, {town.CountGoalSC}, {town.CountGoalCo}");
                }
            }
        }
        static void FindInfoFromCountWin()
        {
            using (var context= new ClubContext())
            {
                var countWin = 30;
                var win = context.Club.SingleOrDefault(t => t.CountWin == countWin);
                if(win!=null)
                {
                    Console.WriteLine($"{win.NameTeam}, {win.TownTeam}, {win.CountDefeats}, {win.CountGames}, {win.CountGoalSC}, {win.CountGoalCo}");
                }              
            }
        }
        static void FindInfoFromCounDefeat()
        {
            using (var context = new ClubContext())
            {
                var countDef = 0;
                var def = context.Club.SingleOrDefault(t => t.CountDefeats == countDef);
                if (def != null)
                {
                    Console.WriteLine($"{def.NameTeam}, {def.TownTeam}, {def.CountWin}, {def.CountGames}, {def.CountGoalSC}, {def.CountGoalCo}");
                }
            }
        }
        static void FindInfoFromCountGoal()
        {
            using (var context = new ClubContext())
            {
                var countGoal = 15;
                var goal = context.Club.SingleOrDefault(t => t.CountGoalSC == countGoal);
                if (goal != null)
                {
                    Console.WriteLine($"{goal.NameTeam}, {goal.TownTeam}, {goal.CountWin}, {goal.CountDefeats}, {goal.CountGames}, {goal.CountGoalCo}");
                }
            }
        }
        static void Add()
        {
            Club club = new Club() { NameTeam = "Shahter", TownTeam = "Donetsk", CountWin = 30, CountDefeats = 0, CountGames = 3, CountGoalSC = 18, CountGoalCo = 3, TicketsSold = 150 };
            Club club1 = new Club() { NameTeam = "Barselona", TownTeam = "Barselona", CountWin = 20, CountDefeats = 5, CountGames = 1, CountGoalSC = 13, CountGoalCo = 6, TicketsSold = 120 };
            Club club2 = new Club() { NameTeam = "Real Madrid", TownTeam = "Madrid", CountWin = 25, CountDefeats = 10, CountGames = 6, CountGoalSC = 9, CountGoalCo = 5, TicketsSold = 100 };
            Club club4 = new Club() { NameTeam = "Dinamo", TownTeam = "Kyiv", CountWin = 18, CountDefeats = 20, CountGames = 4, CountGoalSC = 10, CountGoalCo = 4, TicketsSold = 85 };

            using (var context = new ClubContext())
            {
                context.Club.AddRange(club, club1, club2, club4);
                context.SaveChanges();

                // После сохранения изменений выполните запрос к базе данных
                var presentTeam = context.Club.FirstOrDefault(t => t.NameTeam == "Chelsea");

                if (presentTeam != null)
                {
                    Console.WriteLine("Команда за такою назвою вже існує");
                }
                else
                {
                    Club club3 = new Club() { NameTeam = "Chelsea", TownTeam = "Chelsea", CountWin = 18, CountDefeats = 0, CountGames = 5, CountGoalSC = 15, CountGoalCo = 4, TicketsSold = 130 };
                    context.Club.Add(club3);
                    context.SaveChanges();
                }
            }
        }

        static void Remove(object entity)
        {
            using (var context = new ClubContext())
            {
                context.Remove(entity);

            }
        }

        static void Update(object entity)
        {
            using (var context = new ClubContext())
            {
                context.Update(entity);
            }
        }
    }
}