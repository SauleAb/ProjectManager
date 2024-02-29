namespace LandscapeProjectsManager;

public partial class App : Application
{
	public App()
	{
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF1cXmhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFjW31XcXdWQmFUWUZxVg==");

		InitializeComponent();

		MainPage = new AppShell();
	}
}
