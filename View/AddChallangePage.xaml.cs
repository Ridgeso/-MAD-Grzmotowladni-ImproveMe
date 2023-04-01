using ImproveMe.ViewModel;

namespace ImproveMe.View;

public partial class AddChallangePage : ContentPage
{
	public AddChallangePage(AddChallangeViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}