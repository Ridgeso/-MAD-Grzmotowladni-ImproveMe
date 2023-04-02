
namespace ImproveMe.ViewModel;

[QueryProperty(nameof(User), "User")]
public partial class UserDetailsViewModel : BaseViewModel
{
    UserService _userService;
    ChallangeService _challangeService;
    
    
    [ObservableProperty]
    User user;

    [ObservableProperty]
    int challangesCount;
    public UserDetailsViewModel(UserService service, ChallangeService challangeService)
    {
        _userService = service;
        _challangeService = challangeService;
    }

    partial void OnUserChanged(User value)
    {
        Title = $"{value.Name} {value.LastName}";
        SetChallangesCount();
    }

    public async void SetChallangesCount()
    {
        ChallangesCount = (await _challangeService.GetChellenges()).Count();
    }
}

