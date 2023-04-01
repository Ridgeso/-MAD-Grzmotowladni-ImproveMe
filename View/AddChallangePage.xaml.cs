using ImproveMe.ViewModel;

namespace ImproveMe.View;

public partial class AddTaskPage : ContentPage
{
	public AddTaskPage(AddTaskViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}