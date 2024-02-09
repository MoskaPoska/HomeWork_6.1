using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turnir.DAL.Model;

namespace Turnir.DAL
{
    public class ClubContext:DbContext
    {
        public DbSet<Club> Club { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-9EK8D4R\SQLEXPRESS;Initial Catalog=Turnir;Integrated Security=True;Connect Timeout=30;");
        }
    }
}
