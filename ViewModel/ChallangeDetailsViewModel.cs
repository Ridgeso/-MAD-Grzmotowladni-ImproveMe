
namespace ImproveMe.ViewModel;

[QueryProperty(nameof(Challange), "Challange")]
public partial  class ChallangeDetailsViewModel : BaseViewModel
{
    ChallangeService challangeService;

    public ChallangeDetailsViewModel(ChallangeService service)
    {
        challangeService = service;
    }

    [ObservableProperty]
    Challange challange;

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
        await challangeService.UpdateChallange(challange, true);
    }

    [RelayCommand]
    async Task GoToUserDetails()
    {
        await Shell.Current.GoToAsync(nameof(UserDetailsPage), true);
    }

}
