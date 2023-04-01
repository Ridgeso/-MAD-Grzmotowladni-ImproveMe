namespace ImproveMe.View;

public partial class UserDetailsPage : ContentPage
{
	public UserDetailsPage(UserDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}