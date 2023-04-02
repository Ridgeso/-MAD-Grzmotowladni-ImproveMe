namespace ImproveMe.View;

public partial class CouponsPage : ContentPage
{
	public CouponsPage(CouponsViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}