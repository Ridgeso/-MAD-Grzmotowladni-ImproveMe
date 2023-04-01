using ImproveMe.DTO.Badge;
using ImproveMe.DTO.Challange;

namespace ImproveMe.Services;

public class ChallangeService
{
    SQLiteAsyncConnection Database;
    private readonly UserService _userService;
    private readonly BadgeService _badgeService;
    public ChallangeService(UserService userService, BadgeService badgeService)
    {
        _userService = userService;
        _badgeService = badgeService;
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Challange>();
    }

    async public Task<Challange> CreateChallangeAsync(CreateChallangeDto dto)
    {
        await Init();
        var challange = new Challange()
        {
            Name = dto.Name,
            Description = dto.Description,
            Start = dto.Start,
            Type = dto.Type,
        };
        await Database.InsertAsync(challange);

        var createBadgeDto = new CreateBadgeDto()
        {
            Name = dto.Name,
            ChallangeId = challange.Id,
            Rank = Rank.None,
        };

        var badge = await _badgeService.CreateBadgeAsync(createBadgeDto);
        challange.BadgeId = badge.Id;
        await Database.UpdateAsync(challange);

        return challange;
    }

    public async Task<Challange> GetChallangeAsync()
    {
        await Init();
        return await Database.Table<Challange>().FirstOrDefaultAsync();
    }

    public async Task<int> DeleteChallangeAsync(Challange challange)
    {
        await Init();
        return await Database.DeleteAsync(challange);
    }

    public async Task<List<Challange>> GetChellenges()
    {
        await Init();
        return await Database.Table<Challange>().ToListAsync();
    }
}