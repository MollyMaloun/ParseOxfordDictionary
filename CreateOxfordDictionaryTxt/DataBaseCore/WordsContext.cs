using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CreateOxfordDictionaryTxt.DataBaseCore
{
    internal class WordsContext : DbContext
    {
        public DbSet<Words> engwords { get; set; }
        public WordsContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Words;Username=molly;Password=666");
        }
    }
}
