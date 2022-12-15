// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-27-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="IRepositoryModel.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Models.ModelCar
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using ProyectoXamarin.Interfaces;

	/// <summary>
	/// Defines the <see cref="IRepositoryModel" />.
	/// </summary>
	public interface IRepositoryModel : IRepository<Model>
	{
		/// <summary>
		/// Defines the <see cref="GetByBrandIdAsync" />.
		/// </summary>
		/// <param name="brandId">The id<see cref="int" />.</param>
		/// <returns>The status query<see cref="Task{List{ProyectoXamarin.Models.ModelCar.Model}}" /></returns>
		Task<List<Model>> GetByBrandIdAsync(int brandId);

		/// <summary>
		/// Defines the <see cref="SetModelsIntoDataBase" />.
		/// </summary>
		/// <returns>Task<see cref="Task"/>.</returns>
		Task SetModelsIntoDataBase();
	}
}