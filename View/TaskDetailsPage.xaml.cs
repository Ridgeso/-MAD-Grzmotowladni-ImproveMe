namespace ImproveMe.View;

public partial class TaskDetailsPage : ContentPage
{
	public TaskDetailsPage(TaskDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}