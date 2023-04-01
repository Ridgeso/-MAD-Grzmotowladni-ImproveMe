
namespace ImproveMe.ViewModel;

public partial class UserDetailsViewModel : BaseViewModel
{
    UserService m_UserService;
    
    [ObservableProperty]
    User user;

    public UserDetailsViewModel(UserService service)
    {
        m_UserService = service;
        user = m_UserService.GetUserAsync().Result;
    }

}

