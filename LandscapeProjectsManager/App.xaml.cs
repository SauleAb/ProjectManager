namespace LandscapeProjectsManager;

public partial class App : Application
{
	public App()
	{
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF1cW2hIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFjXH1XcHJVR2JUVUJzXg==");

		InitializeComponent();

		MainPage = new AppShell();
	}
}
