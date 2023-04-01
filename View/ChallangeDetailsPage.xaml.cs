using ImproveMe.ViewModel;
namespace ImproveMe.View;

public partial class ChallangeDetailsPage : ContentPage
{
	public ChallangeDetailsPage(ChallangeDetailsViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}

}