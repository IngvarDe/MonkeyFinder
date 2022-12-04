using MonkeyFinder.View;

namespace MonkeyFinder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        //nameof(DetailsPage) v]rdub DetailsPage

        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
	}
}
