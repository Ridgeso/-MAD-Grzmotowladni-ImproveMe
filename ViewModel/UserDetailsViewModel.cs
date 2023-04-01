
namespace ImproveMe.ViewModel;

[QueryProperty(nameof(User), "User")]
public partial class UserDetailsViewModel : BaseViewModel
{
    UserService m_UserService;
    
    [ObservableProperty]
    User user;


    public UserDetailsViewModel(UserService service)
    {
        m_UserService = service;        
    }
}

