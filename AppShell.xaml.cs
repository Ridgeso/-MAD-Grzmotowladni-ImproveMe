using ImproveMe.View;

namespace ImproveMe;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(AddChallangePage), typeof(AddChallangePage));
		Routing.RegisterRoute(nameof(ChallangeDetailsPage), typeof(ChallangeDetailsPage));
		Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));
		Routing.RegisterRoute(nameof(CouponsPage), typeof(CouponsPage));
    }
}
