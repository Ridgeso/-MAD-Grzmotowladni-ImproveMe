

namespace ImproveMe.ViewModel;

[QueryProperty(nameof(Challange), "Challange")]
public partial  class ChallangeDetailsViewModel : BaseViewModel

{
    UserService m_UserService;
    
    public ChallangeDetailsViewModel(UserService userService) 
    {
        m_UserService = userService;
    }
    [ObservableProperty]
    Challange challange;



}
