using ImproveMe.DTO.Badge;
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
        else if (challange.Streak > 182)
        {
            badge.Rank = Rank.Platinum;
        }
        else
        {
            badge.Rank = Rank.Diamond;
        }

        await Database.UpdateAsync(badge);

        return badge;
    }
}
