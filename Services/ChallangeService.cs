using SQLite;

namespace ImproveMe.Services;

public class ChallangeService
{
    SQLiteAsyncConnection Database;

    private readonly UserService _userService;

    List<Challange> m_Challanges;

    public ChallangeService(UserService userService)
    {
        _userService = userService;
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Challange>();

        var user = await GetChallangeAsync();
        if (user is null)
        {
            await CreateChallangeAsync();
        }
    }

    async public Task<Challange> CreateChallangeAsync(CreateChallangeDto dto)
    {
        await Init();
        var challange = new Challange()
        {
            Name= dto.Name,
            Description= dto.Description,
            Start= dto.Start,
            Type= dto.Type,
        };
        await Database.InsertAsync(challange);

        return new Challange();
    }

    public async Task<Challange> CreateChallangeAsync()
    {
        await Init();
        var user = new Challange() {
            Name = "Zadanie",
            Description = "Moja codzienna rutyna",
            Type = ChallangeType.Routine,
            Start = DateTime.MaxValue,
            Checked = DateTime.MinValue
        };

        await Database.InsertAsync(user);
        return await GetChallangeAsync();
    }

    public async Task<Challange> GetChallangeAsync()
    {
        await Init();
        return await Database.Table<Challange>().FirstOrDefaultAsync();
    }

    public async Task<List<Challange>> GetChellenges()
    {
        await Init();
        return await Database.Table<Challange>().ToListAsync();
    }
}
