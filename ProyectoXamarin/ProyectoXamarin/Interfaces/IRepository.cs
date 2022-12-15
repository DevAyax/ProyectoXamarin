// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-27-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="IRepository.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Interfaces
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// Interface IRepository
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRepository<T> where T : class
	{
		/// <summary>
		/// Gets the by identifier asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Task<see cref="Task{T}"/>.</returns>
		Task<T> GetByIdAsync(int id);

		/// <summary>
		/// Gets all asynchronous.
		/// </summary>
		/// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
		/// <returns>Task<see cref="Task{List{T}"/>.</returns>
		Task<List<T>> GetAllAsync(bool forceRefresh = false);
	}
}