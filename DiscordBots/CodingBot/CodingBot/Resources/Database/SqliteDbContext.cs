using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodingBot.Resources.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Spam> spam { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.1", @"Data\");
            Options.UseSqlite($"Data Source={DbLocation}Database.sqlite");
        }
    }
}
