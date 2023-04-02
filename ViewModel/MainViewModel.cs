
namespace ImproveMe.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    public ObservableCollection<Challange> Challanges { get; } = new();
    ChallangeService m_ChallangeService;
    UserService _userService;

    [ObservableProperty]
    bool isRefreshing;

    public MainViewModel(ChallangeService challangeService, UserService userService)
    {
        Title = "ImproveMe";
        m_ChallangeService = challangeService;

        GetChallangesAsync(true);
        _userService = userService;

        givePointsForLoggin();
    }

    [RelayCommand]
    async Task GetChallangesAsync(bool updateCh = false)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var challenges = await m_ChallangeService.GetChellenges();

            if (Challanges.Count != 0)
                Challanges.Clear();

            foreach (var ch in challenges)
            {
                if (updateCh)
                    await m_ChallangeService.UpdateChallange(ch, false);
                Challanges.Add(ch);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Cannot Load Challanges: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToTaskDetails(Challange challange)
    {
        if (challange == null)
            return;

        await Shell.Current.GoToAsync(nameof(ChallangeDetailsPage), true, new Dictionary<string, object>
        {
            { "Challange", challange }
        });
    }

    [RelayCommand]
    async Task GoToUserDetails()
    {
        await Shell.Current.GoToAsync(nameof(UserDetailsPage), true);
    }

    [RelayCommand]
    async Task GoToAddChallange()
    {
        await Shell.Current.GoToAsync(
            nameof(AddChallangePage),
            true,
            new Dictionary<string, object> { }
        );
    }  
    
    [RelayCommand]
    async Task GoToCoupons()
    {
        await Shell.Current.GoToAsync(
            nameof(CouponsPage),
            true
        );
    }

    private async void givePointsForLoggin()
    {
        var user = await _userService.GetUserAsync();
        var span = new TimeSpan(user.LastLogged.Ticks);
        var todaySpan = new TimeSpan(DateTime.Now.Ticks);


        switch((long)todaySpan.TotalDays - (long)span.TotalDays)
        {
            case 0:
                break;
            case 1:
                user.Streak++;
                var lvl = user.Level; 
                user = await _userService.AddExpPoints(user, 100 * user.Streak);
                if (lvl != user.Level) 
                    await Shell.Current.DisplayAlert("Gratulacje!!!", $"Osiągnąłeś/aś {user.Level} poziom!!!", "Dobrze");
                break;
            default:
                user.Streak = 0;
                break;
        }
        user.Coins = 10000;
        user.LastLogged = DateTime.Now;
        await _userService.UpdateUser(user);
    }

}
