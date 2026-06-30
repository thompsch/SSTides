using Android.Runtime;
using Foundation;
using Microsoft.Maui.Hosting;

namespace SalishSeaTides;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}