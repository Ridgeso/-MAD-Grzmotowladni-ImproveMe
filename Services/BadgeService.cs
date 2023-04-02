using ImproveMe.DTO.Badge;
using SQLite;

namespace ImproveMe.Services;

public class BadgeService
{
    SQLiteAsyncConnection Database;
    private readonly UserService _userService;

    public BadgeService(UserService userService)
    {
        _userService = userService;
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Badge>();
    }

    async public Task<Badge> CreateBadgeAsync(CreateBadgeDto dto)
    {
        await Init();

        var badge = new Badge()
        {
            Name = dto.Name,
            ChallangeId = dto.ChallangeId,
            Rank = dto.Rank,
        };

        await Database.InsertAsync(badge);

        return badge;
    }

    public async Task<Badge> UpdateRank(Challange challange)
    {
        await Init();

        var badge = await Database.Table<Badge>().Where(e => e.Id == challange.BadgeId).FirstOrDefaultAsync();

        if(challange.Streak < 7)
        {
            badge.Rank = Rank.None;
        }
        if (challange.Streak > 7)
        {
            badge.Rank = Rank.Bronze;
        }
        else if (challange.Streak > 31)
        {
            badge.Rank = Rank.Silver;
        }
        else if (challange.Streak > 92)
        {
            badge.Rank = Rank.Gold;
        }
        else if (challange.Streak > 182 && challange.Streak < 365)
        {
            badge.Rank = Rank.Platinum;
        }
        else
        {
            badge.Rank = Rank.Diamond;
        }

        await Database.UpdateAsync(badge);

        var user = await _userService.GetUserAsync();

        await _userService.AddExpPoints(user, (uint)Math.Pow(10, (int)badge.Rank+1));
        user.Coins += ((uint)badge.Rank + 1)*500;
        await _userService.UpdateUser(user);

        return badge;
    }
}
