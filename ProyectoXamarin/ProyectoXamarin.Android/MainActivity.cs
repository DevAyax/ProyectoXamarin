using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

namespace ProyectoXamarin.Droid
{
	[Activity(Label = "FindCar",
		Icon = "@mipmap/ic_launcher",
		Theme = "@style/MainTheme",
		NoHistory = true,
		MainLauncher = false,
		ConfigurationChanges = ConfigChanges.ScreenSize |
		ConfigChanges.Orientation |
		ConfigChanges.UiMode |
		ConfigChanges.ScreenLayout |
		ConfigChanges.SmallestScreenSize,
		ScreenOrientation = ScreenOrientation.Locked)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			UserDialogs.Init(this);

			LoadApplication(new App());
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}