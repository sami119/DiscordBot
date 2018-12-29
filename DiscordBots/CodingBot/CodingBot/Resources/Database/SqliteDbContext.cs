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

        //overides the onconfiguring method
        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            //Uses the database we have
            Options.UseSqlite(@"Data Source=D:\My-Zone\IT-Zone\Repos\Projects\DiscordBots\CodingBot\CodingBot\Data\Database.sqlite");
        }
    }
}
