
namespace ImproveMe.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    public ObservableCollection<Challange> Challanges { get; } = new();
    ChallangeService m_ChallangeService;

    [ObservableProperty]
    bool isRefreshing;

    public MainViewModel(ChallangeService challangeService)
    {
        Title = "ImproveMe";
        m_ChallangeService = challangeService;
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

        await Shell.Current.GoToAsync(nameof(TaskDetailsPage), true, new Dictionary<string, object>
        {
            { "Challange", challange }
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
