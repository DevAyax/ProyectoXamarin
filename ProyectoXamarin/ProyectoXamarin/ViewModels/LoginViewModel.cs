// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-23-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-13-2022
// ***********************************************************************
// <copyright file="LoginViewModel.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Users;
using ProyectoXamarin.Views;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
	/// <summary>
	/// Class LoginViewModel.
	/// Implements the <see cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	public class LoginViewModel : BaseViewModel
	{
		/// <summary>
		/// The new user
		/// </summary>
		private User newUser;

		/// <summary>
		/// The user service
		/// </summary>
		private readonly IUserService userService;

		/// <summary>
		/// The email service
		/// </summary>
		private readonly IEmailService emailService;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoginViewModel"/> class.
		/// </summary>
		public LoginViewModel()
		{
			this.userService = DependencyService.Get<IUserService>();
			this.emailService = DependencyService.Get<IEmailService>();

			NewUser = new User();
			
			LoginCommand = new Command(async () => await LoginUser());
			
			RegisterCommand = new Command(async () => await RegisterUser());
			
			ForgotPasswordCommand = new Command(async () => await SendEmail());
			
			GotoRegistUserCommand = new Command(async () => await Shell.Current.GoToAsync($"//{nameof(RegistrationPage)}"));
			
			LogoutCommand = new Command(async () => await LogoutUser());
		}

		public async Task SendEmail()
		{
			try
			{
				UserDialogs.Instance.ShowLoading("Enviando correo...");
				var user = await userService.GetUserAsync(SesionData.userId);
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
		/// Registers the user.
		/// </summary>
		public async Task RegisterUser()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;
				
				UserDialogs.Instance.ShowLoading();

				await userService.SaveAsync(NewUser);

				UserDialogs.Instance.HideLoading();

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

		/// <summary>
		/// Logins the user.
		/// </summary>
		public async Task LoginUser()
		{
			if (IsBusy)
				return;
			if(string.IsNullOrEmpty(NewUser.Email) | string.IsNullOrEmpty(NewUser.Password))
			{
				UserDialogs.Instance.Alert("Los campos deben estar rellenos", "ERROR", "Ok");
			}

			try
			{
				IsBusy = true;
				
				UserDialogs.Instance.ShowLoading();
				
				await userService.LoginUserAsync(NewUser);

				UserDialogs.Instance.HideLoading();

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

		/// <summary>
		/// Updates the user.
		/// </summary>
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

		/// <summary>
		/// Updates the user.
		/// </summary>
		public async Task LogoutUser()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;

				UserDialogs.Instance.ShowLoading();

				await userService.LogoutUserAsync();
				Application.Current.MainPage = new NavigationPage(new LoginPage());

				UserDialogs.Instance.HideLoading();

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

		/// <summary>
		/// Creates new user.
		/// </summary>
		/// <value>The new user.</value>
		public User NewUser
		{
			get => newUser;
			set
			{
				newUser = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets the login command.
		/// </summary>
		/// <value>The login command.</value>
		public Command LoginCommand { get; }

		/// <summary>
		/// Gets the goto regist user command.
		/// </summary>
		/// <value>The goto regist user command.</value>
		public Command GotoRegistUserCommand { get; }

		/// <summary>
		/// Gets the register command.
		/// </summary>
		/// <value>The register command.</value>
		public Command RegisterCommand { get; }

		/// <summary>
		/// Gets or sets the forgot password command.
		/// </summary>
		/// <value>The forgot password command.</value>
		public Command ForgotPasswordCommand { get; set; }

		/// <summary>
		/// Gets or sets the log out command.
		/// </summary>
		/// <value>The forgot password command.</value>
		public Command LogoutCommand { get; set; }
	}
}