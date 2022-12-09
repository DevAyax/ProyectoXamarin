using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace ProyectoXamarin.Droid
{
	[Activity(Label = "FindCar",
		Icon = "@mipmap/ic_launcher",
		Theme = "@style/newTheme",
		MainLauncher = true,
		NoHistory = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class SplashScreen : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			StartActivity(new Intent(Application.Context, typeof(MainActivity)));
		}
	}
}