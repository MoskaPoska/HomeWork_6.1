using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnir.DAL.Model
{
    public class Club
    {
        public int Id { get; set; }
        public string NameTeam { get; set; }
        public string TownTeam { get; set; }
        public int CountWin { get; set; }
        public int CountDefeats { get; set; }
        public int CountGames { get; set; }
        public int CountGoalSC { get; set; }
        public int CountGoalCo { get; set; }
        public int TicketsSold { get; set; }
        public List<Players> Bombardid { get; set;}
    }
}
