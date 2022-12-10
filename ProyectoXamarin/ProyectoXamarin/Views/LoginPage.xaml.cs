using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		private readonly IUserService userService;

		public LoginPage()
		{
			this.userService = DependencyService.Get<IUserService>();
			Task.Run(async () =>
			{
				await userService.InitAsync();
			});

			InitializeComponent();
		}
	}
}