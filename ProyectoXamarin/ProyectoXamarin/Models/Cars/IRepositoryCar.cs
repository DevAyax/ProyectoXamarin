// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-27-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="IRepositoryCar.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ProyectoXamarin.Models.Cars
{
	using System.Threading.Tasks;
	using ProyectoXamarin.Interfaces;

	/// <summary>
	/// Defines the <see cref="ICarRepository" />.
	/// </summary>
	public interface ICarRepository : IRepository<Car>
	{
		/// <summary>
		/// Defines the <see cref="DeleteAsync" />.
		/// </summary>
		/// <param name="id">The id<see cref="int" />.</param>
		/// <returns>The <see cref="Task{int}" /></returns>
		Task<int> DeleteAsync(int id);

		/// <summary>
		/// Defines the <see cref="SaveCarAsync" />.
		/// </summary>
		/// <param name="car">The car object<see cref="Car" />.</param>
		/// <returns>The status query<see cref="Task{int}" /></returns>
		Task<int> SaveCarAsync(Car car);

		/// <summary>
		/// Defines the <see cref="UpdateCarAsync" />.
		/// </summary>
		/// <param name="car">The id<see cref="Car" />.</param>
		/// <returns>The status query<see cref="Task{int}" /></returns>
		Task<int> UpdateCarAsync(Car car);

		/// <summary>
		/// Defines the <see cref="GetCarByUserAsync" />.
		/// </summary>
		/// <param name="userId">The id<see cref="int" />.</param>
		/// <returns>The Car object<see cref="Task{ProyectoXamarin.Models.Cars.Car}" /></returns>
		Task<Car> GetCarByUserAsync(int userId);
	}
}