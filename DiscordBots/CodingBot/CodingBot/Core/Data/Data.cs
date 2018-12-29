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
        public static ulong GetMessagesAmount(ulong UserId)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.spam.Where(x=>x.UserId == UserId).Count() < 1)
                {
                    return 0;
                }
                return DbContext.spam.Where(x => x.UserId == UserId).Select(x => x.MessagesSend).FirstOrDefault();
            }
        }

        public static async Task SaveMessagesAmount(ulong UserId, uint Amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if(DbContext.spam.Where(x => x.UserId == UserId).Count()<1)
                {
                    DbContext.spam.Add(new Spam
                    {
                        UserId = UserId,
                        MessagesSend = Amount
                    });
                }
                else
                {
                    Spam Curent = DbContext.spam.Where(x => x.UserId == UserId).FirstOrDefault();
                    Curent.MessagesSend += Amount;
                    DbContext.spam.Update(Curent);
                }
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
