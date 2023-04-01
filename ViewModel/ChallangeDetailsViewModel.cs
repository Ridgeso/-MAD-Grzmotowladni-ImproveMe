

using CoreML;

namespace ImproveMe.ViewModel;

[QueryProperty(nameof(Challange), "Challange")]
public partial  class ChallangeDetailsViewModel : BaseViewModel

{

    [ObservableProperty]
    Challange challange;

}
