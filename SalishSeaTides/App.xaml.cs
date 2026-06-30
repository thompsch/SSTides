using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SalishSeaTides;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JEaF5cXmRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXhfeXRVRmddVkVzX0ZWYEk=");
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}