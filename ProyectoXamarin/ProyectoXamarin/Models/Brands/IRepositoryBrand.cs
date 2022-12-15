// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-27-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="IRepositoryBrand.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Models.Brands
{
	using System.Threading.Tasks;
	using ProyectoXamarin.Interfaces;

	/// <summary>
	/// Defines the <see cref="IBrandRepository" />.
	/// </summary>
	public interface IBrandRepository : IRepository<Brand>
	{
		/// <summary>
		/// Defines the <see cref="SetBrandsIntoDataBase" />.
		/// </summary>
		/// <returns>Task.</returns>
		Task SetBrandsIntoDataBase();
	}
}