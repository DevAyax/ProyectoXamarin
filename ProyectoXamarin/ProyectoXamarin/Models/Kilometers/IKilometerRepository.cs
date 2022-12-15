// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-05-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="IKilometerRepository.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Models.Kilometers
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using ProyectoXamarin.Interfaces;

	/// <summary>
	/// Defines the <see cref="IKilometerRepository" />.
	/// </summary>
	public interface IKilometerRepository : IRepository<Kilometer>
	{
		/// <summary>
		/// Defines the <see cref="SaveAsync" />.
		/// </summary>
		/// <param name="kilometer">The id<see cref="Kilometer" />.</param>
		/// <returns>The status query<see cref="Task{ProyectoXamarin.Models.Kilometers.Kilometer}" /></returns>
		Task<int> SaveAsync(Kilometer kilometer);

		/// <summary>
		/// Defines the <see cref="GetAsync" />.
		/// </summary>
		/// <param name="kilometer">The id<see cref="Kilometer" />.</param>
		/// <returns>The status query<see cref="Task{ProyectoXamarin.Models.Kilometers.Kilometer}" /></returns>
		Task<Kilometer> GetAsync(Kilometer kilometer);

		/// <summary>
		/// Defines the <see cref="UpdateSesionDataKilometers" />.
		/// </summary>
		/// <param name="status">The id<see cref="int" />.</param>
		/// <param name="kilometer">The kilometer<see cref="Kilometer" />.</param>
		/// <returns>Task<see cref="Task"/>.</returns>
		Task UpdateSesionDataKilometers(int status, Kilometer kilometer);

		/// <summary>
		/// Gets the by car identifier.
		/// </summary>
		/// <param name="carId">The car identifier.</param>
		/// <returns>Task <see cref="Task{List{ProyectoXamarin.Models.Kilometers.Kilometer}"/>.</returns>
		Task<List<Kilometer>> GetByCarId(int carId);
	}
}