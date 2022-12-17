// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-10-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-17-2022
// ***********************************************************************
// <copyright file="ILocationService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Interfaces
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using ProyectoXamarin.Models.Maps;
	using Xamarin.Essentials;

	/// <summary>
	/// Interface ILocationService
	/// Extends the <see cref="ProyectoXamarin.Interfaces.IService{ProyectoXamarin.Models.Maps.LocationCar}" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Interfaces.IService{ProyectoXamarin.Models.Maps.LocationCar}" />
	public interface ILocationService :IService<LocationCar>
	{
		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		Task DeleteAsync(int id);

		/// <summary>
		/// Gets the location data asynchronous.
		/// </summary>
		/// <returns>The Task<see cref="Task{ProyectoXamarin.Models.Maps.LocationCar}" />.</returns>
		Task<LocationCar> GetLocationDataAsync();

		/// <summary>
		/// Gets the current location.
		/// </summary>
		/// <param name="NewLocation">Creates new location.</param>
		/// <returns>The Task<see cref="Task{ProyectoXamarin.Models.Maps.LocationCar}" />.</returns>
		Task<LocationCar> GetCurrentLocation(LocationCar NewLocation);

		/// <summary>
		/// Navigates to location car.
		/// </summary>
		/// <param name="placemark">The placemark.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		Task NavigateToLocationCar(Placemark placemark);

		/// <summary>
		/// Gets the place mark information.
		/// </summary>
		/// <param name="placemark">The placemark.</param>
		/// <returns>System.String.</returns>
		string GetPlaceMarkInfo(Placemark placemark);

		/// <summary>
		/// Gets all locations asynchronous.
		/// </summary>
		/// <returns>Task&lt;List&lt;LocationCar&gt;&gt;.</returns>
		Task<List<LocationCar>> GetAllLocationsAsync();


		/// <summary>
		/// Gets the place mark from BBDD.
		/// </summary>
		/// <param name="location">The location.</param>
		/// <returns>Placemark.</returns>
		Placemark GetPlaceMarkFromBBDD(LocationCar location);
	}
}