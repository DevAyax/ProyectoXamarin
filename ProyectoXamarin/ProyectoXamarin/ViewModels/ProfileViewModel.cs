// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-17-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-18-2022
// ***********************************************************************
// <copyright file="ProfileViewModel.cs" company="ProyectoXamarin">
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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
	/// <summary>
	/// Class ProfileViewModel.
	/// Implements the <see cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	public class ProfileViewModel : BaseViewModel
	{
		/// <summary>
		/// The user
		/// </summary>
		private User user;

		/// <summary>
		/// The new name
		/// </summary>
		private string name;

		/// <summary>
		/// The new surnames
		/// </summary>
		private string surnames;

		/// <summary>
		/// The new surnames
		/// </summary>
		private string email;

		/// <summary>
		/// The new surnames
		/// </summary>
		private string password;

		/// <summary>
		/// The user service
		/// </summary>
		private readonly IUserService userService;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProfileViewModel"/> class.
		/// </summary>
		public ProfileViewModel()
		{
			this.userService = DependencyService.Get<IUserService>();

			_User = new User();

			LogoutCommand = new Command(async () => await LogoutUser());

			ModifyUserCommand = new Command(async () => await UpdateUser());
		}

		/// <summary>
		/// On appearing as an asynchronous operation.
		/// </summary>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task OnAppearingAsync()
		{
			UserDialogs.Instance.ShowLoading();
			if (SesionData.UserId == 0)
				await userService.AutoLoginAsync();

			SetParamsToProfile();

			UserDialogs.Instance.HideLoading();
		}

		/// <summary>
		/// Sets the parameters to profile.
		/// </summary>
		public async void SetParamsToProfile()
		{
			var email = await SecureStorage.GetAsync(SesionData.Email);
			var user = await userService.GetUserByEmailAsync(email);

			Name = user.Name;
			Email = user.Email;
			Surnames = user.Surname;
			Password = user.Password;
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

				var user = await userService.GetUserByEmailAsync(Email);

				_User.Name = Name;
				_User.Email = Email;
				_User.Surname = Surnames;
				_User.Password = Password;
				_User.Id = user.Id;
				_User.CarId = user.CarId;

				UserDialogs.Instance.ShowLoading();

				await userService.UpdateAsync(_User);
				Application.Current.MainPage = new AppShell();

				UserDialogs.Instance.HideLoading();

				IsBusy = false;
			}
			catch (Exception ex)
			{
				IsBusy = false;
				await UserDialogs.Instance.AlertAsync(ex.StackTrace, "ERROR", "OK");
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
				IsBusy = false;
				await UserDialogs.Instance.AlertAsync(ex.StackTrace, "ERROR", "OK");
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
		public User _User
		{
			get => user;
			set
			{
				user = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the log out command.
		/// </summary>
		/// <value>The forgot password command.</value>
		public Command LogoutCommand { get; set; }

		/// <summary>
		/// Gets or sets modify user.
		/// </summary>
		/// <value>The forgot password command.</value>
		public Command ModifyUserCommand { get; set; }

		/// <summary>
		/// Gets or sets the Name.
		/// </summary>
		/// <value>The Name.</value>
		public string Name
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the Surnames.
		/// </summary>
		/// <value>The surnames.</value>
		public string Surnames
		{
			get => surnames;
			set
			{
				surnames = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The Email.</value>
		public string Email
		{
			get => email;
			set
			{
				email = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password
		{
			get => password;
			set
			{
				password = value;
				OnPropertyChanged();
			}
		}
	}
}