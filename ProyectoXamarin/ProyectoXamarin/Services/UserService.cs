using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Enums;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Users;
using ProyectoXamarin.Services;
using ProyectoXamarin.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserService))]

namespace ProyectoXamarin.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository userRepository;

		private readonly IUtilities utilities;

		public UserService()
		{
			this.userRepository = DependencyService.Get<IUserRepository>();
			this.utilities = DependencyService.Get<IUtilities>();
		}

		public async Task InitAsync()
		{
			var users = await userRepository.GetAllAsync();

			if (users.Any())
			{
				var user = users.OrderByDescending(c => c.Id).FirstOrDefault();
				SesionData.UserId = user.Id;
				await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
			}
		}

		public async Task SaveAsync(User user)
		{
			var exist = await ExistAsync(user);

			if (exist)
			{
				UserDialogs.Instance.Toast($"Bienvenido {user.Email}", TimeSpan.FromSeconds(2));

				await userRepository.UpdateSesionDataUser((int) StatusEnum.Registered, user);

				await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
			}
			else if (!string.IsNullOrEmpty(user.Email) & !string.IsNullOrEmpty(user.Password))
			{
				var statusUser = await userRepository.SaveItemAsync(user);

				await utilities.GetSatatus(statusUser, "Usuario");

				await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
			}
			else
			{
				UserDialogs.Instance.Alert("Debe tener los campos correctamente rellenos", "Error", "OK");
			}
		}

		public async Task<bool> ExistAsync(User user)
		{
			var users = await userRepository.GetAllAsync();
			bool isExist = users.Select(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();

			return isExist;
		}

		public async Task UpdateAsync(User entity)
		{
			var status = await userRepository.UpdateItemAsync(entity);
			await utilities.GetSatatus(status, "Usuario");
		}

		public async Task<User> GetUserAsync(int id)
		{
			return await userRepository.GetByIdAsync(id);
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			return await userRepository.GetAllAsync();
		}
	}
}