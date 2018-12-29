using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodingBot.Resources.Database;
using System.Linq;

namespace CodingBot.Core.Data
{
    public static class Data
    {
        //stores the messages sent in real time
        public static uint MessageAmmount;

        //Returns the sended messages
        public static ulong GetMessagesAmount(ulong UserId)
        {
            //uses the Database
            using (var DbContext = new SqliteDbContext())
            {
                //sees if there arent any messages and returns 0
                if (DbContext.spam.Where(x=>x.UserId == UserId).Count() < 1)
                {
                    return 0;
                }
                //returns the amount pf messages of that user in the database
                return DbContext.spam.Where(x => x.UserId == UserId).Select(x => x.MessagesSend).FirstOrDefault();
            }
        }

        //Saves the messages amount
        public static async Task SaveMessagesAmount(ulong UserId, uint Amount, string name)
        {
            //uses the Database
            using (var DbContext = new SqliteDbContext())
            {
                //sees if there isnt a record for that user and makes one
                if(DbContext.spam.Where(x => x.UserId == UserId).Count()<1)
                {
                    DbContext.spam.Add(new Spam
                    {
                        UserId = UserId,
                        MessagesSend = Amount,
                        Name = name
                    });
                }
                //if there is a record updates the curent Amount
                else
                {
                    Spam Curent = DbContext.spam.Where(x => x.UserId == UserId).FirstOrDefault();
                    Curent.MessagesSend += Amount;
                    DbContext.spam.Update(Curent);
                }

                //restores the real time message amount to 0
                MessageAmmount = 0;

                //Syncs the Database
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
