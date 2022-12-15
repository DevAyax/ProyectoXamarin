// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-10-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="LocationCarViewModel.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Maps;
using ProyectoXamarin.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
	/// <summary>
	/// Class LocationCarViewModel.
	/// Implements the <see cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	public partial class LocationCarViewModel : BaseViewModel
	{
		/// <summary>
		/// The new location
		/// </summary>
		private LocationCar newLocation;

		/// <summary>
		/// The location service
		/// </summary>
		private readonly ILocationService locationService;
		
		private readonly IUserService userService;

		/// <summary>
		/// Initializes a new instance of the <see cref="LocationCarViewModel"/> class.
		/// </summary>
		public LocationCarViewModel()
		{
			this.locationService = DependencyService.Get<ILocationService>();
			this.userService = DependencyService.Get<IUserService>();

			NewLocation = new LocationCar();
			
			SavePositionCommand = new Command(async () => await SavePosition());
			
			InfoCommand = new Command(async () => await GetInfoAsync());
			
			HowToGetCommand = new Command(async () => await HowToGetMyCar());
		}

		/// <summary>
		/// On appearing as an asynchronous operation.
		/// </summary>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task OnAppearingAsync()
		{
			try
			{
				UserDialogs.Instance.ShowLoading();

				if (SesionData.userId == 0)
					await userService.AutoLoginAsync();

				NewLocation = await locationService.GetCurrentLocation(NewLocation);

				UserDialogs.Instance.HideLoading();
			}
			catch (FeatureNotSupportedException fnsEx)
			{
				UserDialogs.Instance.Alert(fnsEx.StackTrace, "ERROR", "OK");
			}
			catch (FeatureNotEnabledException fneEx)
			{
				UserDialogs.Instance.Alert(fneEx.StackTrace, "ERROR", "OK");
			}
			catch (PermissionException pEx)
			{
				UserDialogs.Instance.Alert(pEx.StackTrace, "ERROR", "OK");
			}
			catch (Exception ex)
			{
				UserDialogs.Instance.Alert(ex.StackTrace, "ERROR", "OK");
			}
		}

		/// <summary>
		/// Get information as an asynchronous operation.
		/// </summary>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task GetInfoAsync()
		{
			var info = locationService.GetPlaceMarkInfo(SesionData.placemark);

			await UserDialogs.Instance.ConfirmAsync(info, "Info Location", "OK");
		}

		/// <summary>
		/// Saves the position.
		/// </summary>
		public async Task SavePosition()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;
				
				UserDialogs.Instance.ShowLoading();
				
				await locationService.SaveAsync(NewLocation);
				
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
		/// Hows to get my car.
		/// </summary>
		public async Task HowToGetMyCar()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;

				UserDialogs.Instance.ShowLoading();
				
				await locationService.NavigateToLocationCar(SesionData.placemark);

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
		/// Gets the save position command.
		/// </summary>
		/// <value>The save position command.</value>
		public Command SavePositionCommand { get; }
		/// <summary>
		/// Gets the information command.
		/// </summary>
		/// <value>The information command.</value>
		public Command InfoCommand { get; }
		/// <summary>
		/// Gets the how to get command.
		/// </summary>
		/// <value>The how to get command.</value>
		public Command HowToGetCommand { get; }

		/// <summary>
		/// Creates new location.
		/// </summary>
		/// <value>The new location.</value>
		public LocationCar NewLocation
		{
			get => newLocation;
			set
			{
				newLocation = value;
				OnPropertyChanged();
			}
		}
	}
}