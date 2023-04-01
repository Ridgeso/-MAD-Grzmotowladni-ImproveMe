using ImproveMe.Model;
using SQLite;

namespace ImproveMe.Services;

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

        var user = await GetUserAsync();
        if(user is null)
        {
            await CreateUserAsync();
        }
    }

    public async Task<User> GetUserAsync()
    {
        await Init();
        return await Database.Table<User>().FirstOrDefaultAsync();

    }

    public async Task<User> CreateUserAsync()
    {
        await Init();
        var user = new User()
        {
            Name = "Jan",
            LastName = "Kowalski",
            Points = 0,
            Level = 1,
            LastLogged = new(),
            Streak = 0
        };

        await Database.InsertAsync(user);
        return await GetUserAsync();
    }

}
