// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-10-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="ILocationsRepository.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Models.Maps
{
	using System.Threading.Tasks;
	using ProyectoXamarin.Interfaces;

	/// <summary>
	/// Defines the <see cref="ILocationsRepository" />.
	/// </summary>
	public interface ILocationsRepository : IRepository<LocationCar>
	{
		/// <summary>
		/// Defines the <see cref="DeleteAsync" />.
		/// </summary>
		/// <param name="id">The id<see cref="int" />.</param>
		/// <returns>The status query<see cref="Task{int}" /></returns>
		Task<int> DeleteAsync(int id);

		/// <summary>
		/// Defines the <see cref="DeleteAllAsync" />.
		/// </summary>
		/// <returns>The status query<see cref="Task{int}" /></returns>
		Task<int> DeleteAllAsync();

		/// <summary>
		/// Defines the <see cref="GetLastLocationsByUser" />.
		/// </summary>
		/// <param name="id">The id<see cref="int" />.</param>
		/// <returns>The <see cref="Task{ProyectoXamarin.Models.Maps.LocationCar}" /></returns>
		Task<LocationCar> GetLastLocationsByUser(int id);

		/// <summary>
		/// Defines the <see cref="SaveAsync" />.
		/// </summary>
		/// <param name="locationCar">The id<see cref="LocationCar" />.</param>
		/// <returns>The status query<see cref="Task{int}" /></returns>
		Task<int> SaveAsync(LocationCar locationCar);
	}
}