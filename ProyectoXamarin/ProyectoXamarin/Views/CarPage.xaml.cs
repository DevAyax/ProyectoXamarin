using System.Runtime.CompilerServices;
using ProyectoXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarPage : ContentPage
	{
		public CarPage()
		{
			InitializeComponent();
			this.BindingContext = new CreateCarViewModel();
		}

		protected override async void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if (BindingContext is CreateCarViewModel createCarViewModel)
			{
				await createCarViewModel.OnActivatedAsync();
			}
		}
	}
}