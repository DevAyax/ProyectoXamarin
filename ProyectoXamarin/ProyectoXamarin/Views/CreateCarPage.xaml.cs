using ProyectoXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateCarPage : ContentPage
	{
		public CreateCarPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			if (BindingContext is CreateCarViewModel createCarViewModel)
			{
				await createCarViewModel.OnAppearingAsync();
			}
		}
	}
}