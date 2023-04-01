
namespace ImproveMe.ViewModel;

[QueryProperty(nameof(User), "User")]
public partial class UserDetailsViewModel
{
    UserService m_UserService;
    
    [ObservableProperty]
    User m_CurrentUser;


    public UserDetailsViewModel(UserService service)
    {
        m_UserService = service;        
    }
}

