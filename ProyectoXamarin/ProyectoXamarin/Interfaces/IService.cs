// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="IService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Interfaces
{
	using System.Threading.Tasks;

	/// <summary>
	/// Interface IService
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IService<T> where T : class
	{
		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		Task UpdateAsync(T entity);

		/// <summary>
		/// Saves the asynchronous.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		Task SaveAsync(T entity);
	}
}