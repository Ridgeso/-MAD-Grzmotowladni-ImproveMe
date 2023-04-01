using ImproveMe.DTO.Badge;
using SQLite;

namespace ImproveMe.Services
{
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
    }

}
