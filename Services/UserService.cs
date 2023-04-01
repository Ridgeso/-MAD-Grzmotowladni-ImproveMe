using ImproveMe.Model;
using SQLite;

namespace ImproveMe.Services;

public class UserService
{
    private SQLiteAsyncConnection Database;
    public UserService() {
    }
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
            LastLogged = DateTime.Now,
            Streak = 0
        };

        await Database.InsertAsync(user);
        return await GetUserAsync();
    }

    public async Task UpdateUser(User user)
    {
        await Init();
        await Database.UpdateAsync(user);
    }

    public async Task<User> AddExpPoints(User user, uint exp)
    {
        await Init();

        user.Points += exp;

        user.Level = CalcLevel(user.Points, user.Level);

        await UpdateUser(user);

        return user;
    }

    private ushort CalcLevel(uint exp, ushort level)
    {
        return (ushort)((5+Math.Sqrt(25+2*exp))/10);
    }


}
