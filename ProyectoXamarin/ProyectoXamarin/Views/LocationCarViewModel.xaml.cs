using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.ViewModels
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationCarViewModelxaml : ContentPage
	{
		public double latitude { get; set; }
		public double longitude { get; set; }

		public LocationCarViewModelxaml(double _latitude, double _longitude)
		{
			InitializeComponent();
			latitude = _latitude;
			longitude = _longitude;
		}

		protected override void OnAppearing()
		{
			var pin = new Pin
			{
				Type = PinType.SavedPin,
				Label = "Aquí dejaste tu coche",
				Position = new Position(latitude, longitude),
			};
			//map.Pins.Add(pin);
			//base.OnAppearing();
		}
	}
}