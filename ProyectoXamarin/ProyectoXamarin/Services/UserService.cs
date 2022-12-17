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
		/// The email service
		/// </summary>
		private readonly IEmailService emailService;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserService" /> class.
		/// </summary>
		public UserService()
		{
			this.userRepository = DependencyService.Get<IUserRepository>();
			this.emailService = DependencyService.Get<IEmailService>();
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
				SesionData.UserId = user.Id;
				isRegistered = true;
			}

			return isRegistered;
		}

		public async Task SendEmailAsync()
		{
			try
			{
				UserDialogs.Instance.ShowLoading("Enviando correo...");

				var email = await SecureStorage.GetAsync(SesionData.Email);
				var user = await userRepository.GetUserByEmailAsync(email);

				var result = await emailService.SendEmailAsync(user.Email, user.Name, user.Password);
				
				var status = result.StatusCode;

				if (result.StatusCode == System.Net.HttpStatusCode.OK)
				{
					UserDialogs.Instance.HideLoading();
					await UserDialogs.Instance.AlertAsync("Correo enviado correctamente, compruebe su bandeja de entra y spam", "INFO", "OK");
				}
			}
			catch (System.Exception ex)
			{
				await UserDialogs.Instance.AlertAsync(ex.StackTrace, "ERROR", "OK");
			}
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

				RemoveSessionDataAsync();
				
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
				
				var userRegister = await userRepository.GetUserByEmailAsync(user.Email);
				SesionData.UserId = userRegister.Id;

				await SecureStorage.SetAsync(SesionData.Email, user.Email);

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
			var users = await GetAllUsersAsync();
			var isExist = users.Any(u => u.Email == user.Email);
			var passIsOk = users.Any(u => u.Email == user.Email & u.Password == user.Password);

			if (isExist & !passIsOk)
			{
				var result = await UserDialogs.Instance.ConfirmAsync("El password no coincide, ¿Quíere recibir un email con su password", "Error", "OK", "CANCELAR");
				
				if(result)
					await SendEmailAsync();
			}

			bool isOk = isExist && passIsOk;

			return isOk;
		}

		/// <summary>
		/// Update as an asynchronous operation.
		/// </summary>
		/// <param name="user">The entity.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task UpdateAsync(User user)
		{
			var status = await userRepository.UpdateItemAsync(user);

			status = status == 1 ? (int) StatusEnum.Updated : (int) StatusEnum.ErrorInProcess;

			await utilities.GetStatus(status, nameof(EntityEnums.User));

			Preferences.Remove(SesionData.SessionKey);

			RemoveSessionDataAsync();

			Preferences.Set(SesionData.SessionKey, DateTime.UtcNow);

			await SetSessionDataAsync(user);
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


		public async Task SetSessionDataAsync(User user)
		{
			await SecureStorage.SetAsync(SesionData.Email, user.Email);
			await SecureStorage.SetAsync(SesionData.Name, user.Name);
			await SecureStorage.SetAsync(SesionData.Surnames, user.Surname);
			await SecureStorage.SetAsync(SesionData.Password, user.Password);
		}

		public void RemoveSessionDataAsync()
		{
			SecureStorage.RemoveAll();
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await userRepository.GetUserByEmailAsync(email);
		}
	}
}