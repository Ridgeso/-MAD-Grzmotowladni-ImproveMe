

namespace ImproveMe.ViewModel;

public partial class AddChallangeViewModel : BaseViewModel
{
    public AddChallangeViewModel()
    {
        Title = "Dodaj wyzwanie";
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
