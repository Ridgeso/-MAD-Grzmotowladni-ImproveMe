using ImproveMe.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImproveMe.Services
{
    public class UserService
    {
        SQLiteAsyncConnection Database;
        public UserService() { }
        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<User>();
        }

        public async Task<User> GetUserAsync()
        {
            await Init();
            return await Database.Table<User>().FirstOrDefaultAsync();
        }

    }
}
