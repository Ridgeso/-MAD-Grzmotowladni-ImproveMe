using ImproveMe.ViewModel;
namespace ImproveMe.View;

public partial class TaskDetailsPage : ContentPage
{
	public TaskDetailsPage(ChallangeDetailsViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}

}