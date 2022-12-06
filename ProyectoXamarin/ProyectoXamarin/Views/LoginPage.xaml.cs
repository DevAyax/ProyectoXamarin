using ProyectoXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			this.BindingContext = new LoginViewModel();
		}
		protected override async void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if(BindingContext is LoginViewModel loginViewModel)
			{
				await loginViewModel.OnActivatedAsync();
			}
		}
	}
}