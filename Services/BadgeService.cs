using SQLite;

namespace ImproveMe.Services;

public class BadgeService
{
    SQLiteAsyncConnection Database;
    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Badge>();
    }

}
