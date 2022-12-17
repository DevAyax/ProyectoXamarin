// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-10-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-12-2022
// ***********************************************************************
// <copyright file="LocationRepository.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Data.Repository;
using ProyectoXamarin.Models.Maps;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationRepository))]

namespace ProyectoXamarin.Data.Repository
{
	/// <summary>
	/// Class LocationRepository.
	/// Implements the <see cref="ILocationsRepository" />
	/// </summary>
	/// <seealso cref="ILocationsRepository" />
	public class LocationRepository : ILocationsRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LocationRepository"/> class.
		/// </summary>
		public LocationRepository()
		{
		}

		/// <summary>
		/// Delete all as an asynchronous operation.
		/// </summary>
		/// <returns>A Task <see cref="Task{int}"/> representing the asynchronous operation.</returns>
		public async Task<int> DeleteAllAsync()
		{
			return await App.DataBase.connect.DeleteAllAsync<LocationCar>();
		}

		/// <summary>
		/// Delete as an asynchronous operation.
		/// </summary>
		/// <param name="id">The id<see cref="int" />.</param>
		/// <returns>A Task<see cref="Task{int}"/> representing the asynchronous operation.</returns>
		public async Task<int> DeleteAsync(int id)
		{
			var location = await App.DataBase.connect.Table<LocationCar>().Where(b => b.Id == id).FirstOrDefaultAsync();

			return await App.DataBase.connect.DeleteAsync(location);
		}

		/// <summary>
		/// Get all as an asynchronous operation.
		/// </summary>
		/// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
		/// <returns>The Task<see cref="Task{List{ProyectoXamarin.Models.Maps.LocationCar}}"/>.</returns>
		public async Task<List<LocationCar>> GetAllAsync(bool forceRefresh = false)
		{
			return await App.DataBase.connect.Table<LocationCar>().ToListAsync();
		}

		/// <summary>
		/// Get by identifier as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The Task<see cref="Task{ProyectoXamarin.Models.Maps.LocationCar}"/>.</returns>
		public async Task<LocationCar> GetByIdAsync(int id)
		{
			var location = await App.DataBase.connect.Table<LocationCar>().Where(b => b.Id == id).FirstOrDefaultAsync();

			return location;
		}

		/// <summary>
		/// Defines the <see cref="GetAsync" />.
		/// </summary>
		/// <param name="id">The id<see cref="int" />.</param>
		/// <returns>The Task<see cref="Task{ProyectoXamarin.Models.Maps.LocationCar}"/>.</returns>
		public async Task<LocationCar> GetLastLocationsByUser(int id)
		{
			return await App.DataBase.connect.Table<LocationCar>().FirstOrDefaultAsync(l => l.UserId == id);
		}

		/// <summary>
		/// Save as an asynchronous operation.
		/// </summary>
		/// <param name="LocationCar">The item.</param>
		/// <returns>The Task<see cref="Task{int}"/>.</returns>
		public async Task<int> SaveAsync(LocationCar LocationCar)
		{
			var status = await App.DataBase.connect.InsertAsync(LocationCar);
			if (status == 1)
				await UpdateSesionDataLocation(LocationCar);

			return status;
		}

		/// <summary>
		/// Updates the sesion data location.
		/// </summary>
		/// <param name="location">The location.</param>
		/// /// <returns>Task<see cref="Task" />.</returns>
		public async Task UpdateSesionDataLocation(LocationCar location)
		{
			var _location = await App.DataBase.connect.Table<LocationCar>().Where(l => l.Latitude == location.Latitude && l.Longitude == location.Longitude).FirstOrDefaultAsync();
			SesionData.latitude = (double) _location.Latitude;
			SesionData.longitude = (double) _location.Longitude;
			SesionData.longitude = (double) _location.Longitude;
			SesionData.CountryName = _location.CountryName;
			SesionData.AdminArea = _location.AdminArea;
			SesionData.Address = _location.Address;
			SesionData.Locality = _location.Locality;
		}
	}
}