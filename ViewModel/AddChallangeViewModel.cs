
namespace ImproveMe.ViewModel
{
    public partial class AddChallangeViewModel : BaseViewModel
    {
        private readonly ChallangeService _challangeService;
        private readonly UserService _userService;

        [ObservableProperty]
        string name;
        
        [ObservableProperty]
        string description;
        
        [ObservableProperty]
        int type;

        [ObservableProperty]
        bool showNameError = false;
        
        [ObservableProperty]
        bool showDescriptionError = false;

        public AddChallangeViewModel(ChallangeService challangeService, UserService userService)
        {
            Title = "Dodaj wyzwanie";

            _challangeService = challangeService;
            _userService = userService;
        }

        [RelayCommand]
        async void Save()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var user = await _userService.GetUserAsync();
                var createChallangeDto = new CreateChallangeDto()
                {
                    Name = Name,
                    Description = Description,
                    Start = new DateTime(),
                    UserId = user.Id,
                    Type = (ChallangeType)Type
                };

                if (Validate()) {
                    var res = await _challangeService.CreateChallangeAsync(createChallangeDto);
                    await Shell.Current.DisplayAlert("Sukces!", "Poprawnie dodano wyzwanie." , "Zatwierdź");
                    await Shell.Current.GoToAsync(nameof(MainPage), true);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Wystąpił błąd!", ex.Message, "Zatwierdź");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool Validate()
        {
            if (Name is null || Name.Length == 0)
            {
                ShowNameError = true;
            }
            else
            {
                ShowNameError = false;
            }

            if (Description is null || Description.Length == 0)
            {
                ShowDescriptionError = true;
            }
            else
            {
                ShowDescriptionError = false;
            }
            return !(ShowNameError || ShowDescriptionError);
        }
    }
}
