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
            Streak = 0,
            LastChecked = dto.Start,
            Period = 1L,
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

    public async Task UpdateChallange(Challange challange, bool Action)
    {
        await Init();
        var difference = DateTime.Now.Day - challange.LastChecked.Day;
        
        if (challange.Type == ChallangeType.Routine && Action)
        {
            if (difference < challange.Period)
                return;

            if (difference == challange.Period)
                challange.Streak++;
            else
                challange.Streak = 0;
            challange.LastChecked = DateTime.Now;
        }
        else if (challange.Type != ChallangeType.Abstinence && !Action)
        {
            if (difference < challange.Period)
            {
                challange.LastChecked = DateTime.Now;
                challange.Streak = 0;
            }
            else
            {
                if (challange.Period > 0)
                    challange.Streak = (long)Math.Ceiling((Decimal)(DateTime.Now.Day - challange.LastChecked.Day) / challange.Period);
                else
                    challange.Streak = 0;
            }
        }

        await Database.UpdateAsync(challange);
    }
}