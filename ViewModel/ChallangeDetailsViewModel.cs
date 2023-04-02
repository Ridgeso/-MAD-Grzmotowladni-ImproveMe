
using CommunityToolkit.Mvvm.ComponentModel;

namespace ImproveMe.ViewModel;

[QueryProperty(nameof(Challange), "Challange")]
public partial class ChallangeDetailsViewModel : BaseViewModel
{
    ChallangeService challangeService;
    UserService userService;

    public ChallangeDetailsViewModel(ChallangeService serviceCh, UserService serviceUs)
    {
        challangeService = serviceCh;
        userService = serviceUs;
    }

    [ObservableProperty]
    Challange challange;

    [ObservableProperty]
    Color m_AcctionColour;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsHidden))]
    bool isVisible;

    public bool IsHidden => !IsVisible;

    [RelayCommand]
    public async Task DeleteChallangeAsync(Challange challangeToDelete)
    {
        var succeded = await challangeService.DeleteChallangeAsync(challangeToDelete);

        if (succeded.Equals(1))
        {
            await Shell.Current.DisplayAlert("Sukces!", "Poprawnie usunięto wyzwanie.", "Zatwierdź");
            await Shell.Current.GoToAsync("..", true);
        }
        else
        {
            Debug.WriteLine($"Something went wrong with deleting: {succeded} files touched.");
            await Shell.Current.DisplayAlert("Error!", $"Something went wrong with deleting: {succeded} files touched.", "OK");
        }
    }

    [RelayCommand]
    async Task ActionAsync()
    {
        bool faild = await challangeService.UpdateChallange(challange, true);

        if (faild)
            AcctionColour = Colors.Red;
        else
            AcctionColour = Colors.Green;

    }

    [RelayCommand]
    async Task GoToUserDetails()
    {
        User user = await userService.GetUserAsync();
        if (user == null)
            return;
        await Shell.Current.GoToAsync(nameof(UserDetailsPage), true, new Dictionary<string, object>
        {
            { "User", user }
        });
    }


    partial void OnChallangeChanged(Challange value)
    {
        IsVisible = Challange.Type == ChallangeType.Routine;
    }
}
