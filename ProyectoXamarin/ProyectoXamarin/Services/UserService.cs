// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-15-2022
// ***********************************************************************
// <copyright file="UserService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Enums;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Users;
using ProyectoXamarin.Services;
using ProyectoXamarin.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserService))]

namespace ProyectoXamarin.Services
{
	/// <summary>
	/// Class UserService.
	/// Implements the <see cref="IUserService" />
	/// </summary>
	/// <seealso cref="IUserService" />
	public class UserService : IUserService
	{
		/// <summary>
		/// The user repository
		/// </summary>
		private readonly IUserRepository userRepository;

		/// <summary>
		/// The utilities
		/// </summary>
		private readonly IUtilities utilities;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserService" /> class.
		/// </summary>
		public UserService()
		{
			this.userRepository = DependencyService.Get<IUserRepository>();
			this.utilities = DependencyService.Get<IUtilities>();
		}

		/// <summary>
		/// Initialize as an asynchronous operation.
		/// </summary>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task<bool> InitAsync()
		{
			var users = await userRepository.GetAllAsync();
			bool isRegistered = false;
			if (users.Any())
			{
				var user = users.FirstOrDefault();
				SesionData.userId = user.Id;
				isRegistered = true;
			}

			return isRegistered;
		}

		/// <summary>
		/// Automatic login as an asynchronous operation.
		/// </summary>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task AutoLoginAsync()
		{
			var email = await SecureStorage.GetAsync(SesionData.Email);
			var user = await userRepository.GetUserByEmailAsync(email);

			UserDialogs.Instance.Toast($"Bienvenido {user.Email}", TimeSpan.FromSeconds(2));

			await userRepository.UpdateSesionDataUser(user);

			Preferences.Set(SesionData.SessionKey, DateTime.UtcNow);

			Application.Current.MainPage = new AppShell();
			
		}

		/// <summary>
		/// Login user as an asynchronous operation.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task LoginUserAsync(User user)
		{
			var exist = await ExistAsync(user);

			if (exist)
			{
				UserDialogs.Instance.Toast($"Bienvenido {user.Email}", TimeSpan.FromSeconds(2));
				
				await userRepository.UpdateSesionDataUser(user);
				
				Preferences.Set(SesionData.SessionKey, DateTime.UtcNow);
				
				await SecureStorage.SetAsync(SesionData.Email, user.Email);
				
				Application.Current.MainPage = new AppShell();
			}
			else
			{
				var confirmConfig = new ConfirmConfig
				{
					Title = "Advertencia",
					Message = "Este usuario no existe, ¿Quiere registrarse?",
					OkText = "Si",
					CancelText = "No"
				};
				bool result = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
				if (result)
				{
					await (Application.Current.MainPage as NavigationPage).PushAsync(new RegistrationPage());
				}
				else
				{
					return;
				}
			}
		}

		/// <summary>
		/// Login user as an asynchronous operation.
		/// </summary>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task LogoutUserAsync()
		{
			var users = await userRepository.GetAllAsync();

			if (users.Any())
			{
				var user = users.FirstOrDefault();

				await userRepository.UpdateSesionDataUser(user);

				Preferences.Remove(SesionData.SessionKey);
				
				SecureStorage.Remove(SesionData.Email);
				
				Application.Current.MainPage = new NavigationPage(new LoginPage());
			}
		}

		/// <summary>
		/// Save as an asynchronous operation.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task SaveAsync(User user)
		{
			var exist = await ExistAsync(user);
			bool fieldsOk = (!string.IsNullOrEmpty(user.Email) & !string.IsNullOrEmpty(user.Password)
				& !string.IsNullOrEmpty(user.Name) & !string.IsNullOrEmpty(user.Surname));

			if (!exist & fieldsOk)
			{
				var status = await userRepository.SaveItemAsync(user);

				status = status == 1 ? (int) StatusEnum.Registered : (int) StatusEnum.ErrorInProcess;

				await utilities.GetStatus(status, nameof(EntityEnums.User));

				await (Application.Current.MainPage as NavigationPage).PopAsync(true);
			}
			else if (exist)
			{
				UserDialogs.Instance.Alert("Ya existe un usuario con este email, pruebe de nuevo con otro", "Error", "OK");
			}
			else if (fieldsOk)
			{
				UserDialogs.Instance.Alert("Debe tener los campos correctamente rellenos", "Error", "OK");
			}
		}

		/// <summary>
		/// Exist as an asynchronous operation.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>A Task<see cref="Task{bool}" /> representing the asynchronous operation.</returns>
		public async Task<bool> ExistAsync(User user)
		{
			return await userRepository.CompareUserAsync(user);
		}

		/// <summary>
		/// Update as an asynchronous operation.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task UpdateAsync(User entity)
		{
			var status = await userRepository.UpdateItemAsync(entity);

			status = status == 1 ? (int) StatusEnum.Updated : (int) StatusEnum.ErrorInProcess;

			await utilities.GetStatus(status, nameof(EntityEnums.User));
		}

		/// <summary>
		/// Get user as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A Task<see cref="Task{ProyectoXamarin.Models.Users.User}" /> representing the asynchronous operation.</returns>
		public async Task<User> GetUserAsync(int id)
		{
			return await userRepository.GetByIdAsync(id);
		}

		/// <summary>
		/// Get all users as an asynchronous operation.
		/// </summary>
		/// <returns>A Task<see cref="Task{List{ProyectoXamarin.Models.Users.User}}" /> representing the asynchronous operation.</returns>
		public async Task<List<User>> GetAllUsersAsync()
		{
			return await userRepository.GetAllAsync();
		}
	}
}