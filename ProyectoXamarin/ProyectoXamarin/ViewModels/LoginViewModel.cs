using System;
using System.Linq;
using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Users;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public Command LoginCommand { get; }
		public Command RegsiterCommand { get; }

		private User newUser;

		private readonly IUserService userService;

		public LoginViewModel()
		{
			this.userService = DependencyService.Get<IUserService>();
			NewUser = new User();
			LoginCommand = new Command(async () => await SaveUser());
			RegsiterCommand = new Command(async () => await SaveUser());
		}

		public async Task OnAppearingAsync()
		{
			var users = await userService.GetAllUsersAsync();

			if (users.Any())
			{
				NewUser = users.OrderByDescending(c => c.Id).FirstOrDefault();
				SesionData.UserId = NewUser.Id;
			}
		}

		public async Task SaveUser()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;

				await userService.SaveAsync(NewUser);

				IsBusy = false;
			}
			catch (Exception ex)
			{
				IsBusy = true;
				throw;
			}
			finally
			{
				IsBusy = false;
			}
		}

		// TODO esto para la ventana de actualización de datos
		public async Task UpdateUser()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;

				await userService.UpdateAsync(NewUser);

				IsBusy = false;
			}
			catch (Exception ex)
			{
				IsBusy = true;
				throw;
			}
			finally
			{
				IsBusy = false;
			}
		}

		public User NewUser
		{
			get => newUser;
			set
			{
				newUser = value;
				OnPropertyChanged();
			}
		}
	}
}