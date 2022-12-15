// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="IDataStore.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Interfaces
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// Interface IDataStore
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDataStore<T>
	{
		/// <summary>
		/// Adds the item asynchronous.
		/// </summary>
		/// <param name="entity">The item.</param>
		/// <returns>Task<see cref="Task{bool}"/>.</returns>
		Task<bool> AddItemAsync(T entity);

		/// <summary>
		/// Updates the item asynchronous.
		/// </summary>
		/// <param name="entity">The item.</param>
		/// <returns>Task<see cref="Task{bool}"/>.</returns>
		Task<bool> UpdateItemAsync(T entity);

		/// <summary>
		/// Deletes the item asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Task<see cref="Task{bool}"/>.</returns>
		Task<bool> DeleteItemAsync(string id);

		/// <summary>
		/// Gets the item asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Task<see cref="Task{T}"/>.</returns>
		Task<T> GetItemAsync(string id);

		/// <summary>
		/// Gets the items asynchronous.
		/// </summary>
		/// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
		/// <returns>Task<see cref="{Task{IEnumerable{T}}"/>.</returns>
		Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
	}
}