namespace ImproveMe;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(AddChallangePage), typeof(AddChallangePage));
		Routing.RegisterRoute(nameof(TaskDetailsPage), typeof(TaskDetailsPage));
		Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));
    }
}
