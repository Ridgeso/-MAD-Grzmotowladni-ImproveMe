
using System.Windows.Input;

namespace ImproveMe.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    public ObservableCollection<Challange> Challanges { get; } = new();
    ChallangeService m_ChallangeService;

    UserService m_UserService;

    [ObservableProperty]
    bool isRefreshing;

    public MainViewModel(ChallangeService challangeService, UserService userService)
    {
        Title = "ImproveMe";
        m_ChallangeService = challangeService;
        m_UserService = userService;
    }

    [RelayCommand]
    async Task GetChallangesAsync()
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
                Challanges.Add(ch);
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

        User user = await m_UserService.GetUserAsync();

        await Shell.Current.GoToAsync(nameof(UserDetailsPage), true, new Dictionary<string, object>
        {
            { "User", user }
        });
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


}
