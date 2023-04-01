namespace ImproveMe;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(AddChallangeViewModel), typeof(AddChallangeViewModel));
		Routing.RegisterRoute(nameof(TaskDetailsViewModel), typeof(TaskDetailsViewModel));
		Routing.RegisterRoute(nameof(UserDetailsViewModel), typeof(UserDetailsViewModel));
    }
}
