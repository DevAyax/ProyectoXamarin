// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-10-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-17-2022
// ***********************************************************************
// <copyright file="LocationService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Enums;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Maps;
using ProyectoXamarin.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationService))]

namespace ProyectoXamarin.Services
{
	/// <summary>
	/// Class LocationService.
	/// Implements the <see cref="ILocationService" />
	/// </summary>
	/// <seealso cref="ILocationService" />
	public class LocationService : ILocationService
	{
		/// <summary>
		/// The locations repository
		/// </summary>
		private readonly ILocationsRepository locationsRepository;
		/// <summary>
		/// The utilities
		/// </summary>
		private readonly IUtilities utilities;

		/// <summary>
		/// Initializes a new instance of the <see cref="LocationService" /> class.
		/// </summary>
		public LocationService()
		{
			this.locationsRepository = DependencyService.Get<ILocationsRepository>();
			this.utilities = DependencyService.Get<IUtilities>();
		}

		/// <summary>
		/// Delete as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task DeleteAsync(int id)
		{
			if (id != 0)
			{
				var status = await locationsRepository.DeleteAsync(id);
				status = status == 1 ? (int) StatusEnum.Deleted : (int) StatusEnum.ErrorInProcess;
				await utilities.GetStatus(status, nameof(EntityEnums.Location));
			}
		}

		/// <summary>
		/// Get location data as an asynchronous operation.
		/// </summary>
		/// <returns>A Task<see cref="Task{ProyectoXamarin.Models.Maps.LocationCar}" /> representing the asynchronous operation.</returns>
		public async Task<LocationCar> GetLocationDataAsync()
		{
			return await locationsRepository.GetLastLocationsByUser(SesionData.UserId);
		}

		/// <summary>
		/// Get all locations as an asynchronous operation.
		/// </summary>
		/// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
		public async Task<List<LocationCar>> GetAllLocationsAsync()
		{
			return await locationsRepository.GetAllAsync();
		}

		/// <summary>
		/// Save as an asynchronous operation.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task SaveAsync(LocationCar entity)
		{
			await App.DataBase.connect.DeleteAllAsync<LocationCar>();
			await locationsRepository.SaveAsync(entity);
		}

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>Task<see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public Task UpdateAsync(LocationCar entity)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the current location.
		/// </summary>
		/// <param name="NewLocation">Creates new location.</param>
		/// <returns>The Task<see cref="Task{ProyectoXamarin.Models.Maps.LocationCar}" />.</returns>
		public async Task<LocationCar> GetCurrentLocation(LocationCar NewLocation)
		{
			UserDialogs.Instance.ShowLoading("Cargando...");
			var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5));
			var cts = new CancellationTokenSource();
			var location = await Geolocation.GetLocationAsync(request, cts.Token);

			if (location != null)
			{
				var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
				var placemark = placemarks?.FirstOrDefault();

				if (placemark != null)
				{
					NewLocation.Longitude = location.Longitude;
					NewLocation.Latitude = location.Latitude;
					NewLocation.Locality = placemark.Locality;
					NewLocation.AdminArea = placemark.AdminArea;
					NewLocation.CountryName = placemark.CountryName;
					NewLocation.UserId = SesionData.UserId;

					var pM = GetPlaceMarkInfo(placemark);

					SesionData.Placemark = placemark;
				}
			}
			UserDialogs.Instance.HideLoading();
			return NewLocation;
		}

		/// <summary>
		/// Gets the place mark information.
		/// </summary>
		/// <param name="placemark">The placemark.</param>
		/// <returns>System.String.</returns>
		public string GetPlaceMarkInfo(Placemark placemark)
		{
			return
					$"AdminArea:       {placemark.AdminArea}\n" +
					$"CountryCode:     {placemark.CountryCode}\n" +
					$"CountryName:     {placemark.CountryName}\n" +
					$"FeatureName:     {placemark.FeatureName}\n" +
					$"Locality:        {placemark.Locality}\n" +
					$"PostalCode:      {placemark.PostalCode}\n" +
					$"SubAdminArea:    {placemark.SubAdminArea}\n" +
					$"SubLocality:     {placemark.SubLocality}\n" +
					$"SubThoroughfare: {placemark.SubThoroughfare}\n" +
					$"Thoroughfare:    {placemark.Thoroughfare}\n";
		}

		/// <summary>
		/// Gets the place mark information.
		/// </summary>
		/// <param name="placemark">The placemark.</param>
		/// <returns>System.String.</returns>
		public Placemark GetPlaceMarkFromBBDD(LocationCar location)
		{
			var placemark = new Placemark 
			{
				CountryName = location.CountryName,					   
				AdminArea = location.AdminArea,
				Locality = location.Locality,
				Location = new Location { Longitude = (double)location.Longitude, Latitude = (double)location.Latitude},
			};
			return placemark;
		}

		/// <summary>
		/// Navigates to location car.
		/// </summary>
		/// <param name="placemark">The placemark.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		public async Task NavigateToLocationCar(Placemark placemark)
		{
			var options = new MapLaunchOptions { Name = "Ubicación de mi coche aparcado", NavigationMode = NavigationMode.Walking };

			await Map.OpenAsync(placemark, options);
		}
	}
}