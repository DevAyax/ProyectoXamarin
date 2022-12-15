// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-28-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="IRepositoryUser.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ProyectoXamarin.Models.Users
{
	using System;
	using System.Threading.Tasks;
	using ProyectoXamarin.Interfaces;

	/// <summary>
	/// Defines the <see cref="IUserRepository" />.
	/// </summary>
	public interface IUserRepository : IRepository<User>
	{
		/// <summary>
		/// Deletes the user asynchronous.
		/// </summary>
		/// <param name="id">The id<see cref="int"/>.</param>
		/// <returns>The status query<see cref="Task{int}"/></returns>
		Task<int> DeleteUserAsync(int id);

		/// <summary>
		/// Saves the item asynchronous.
		/// </summary>
		/// <param name="item">The user<see cref="User"/>.</param>
		/// <returns>The status query<see cref="Task{int}"/></returns>
		Task<int> SaveItemAsync(User user);

		/// <summary>
		/// Updates the item asynchronous.
		/// </summary>
		/// <param name="user">The user<see cref="User"/>.</param>
		/// <returns>The status query<see cref="Task{int}"/></returns>
		Task<int> UpdateItemAsync(User user);

		/// <summary>
		/// Updates the sesion data user.
		/// </summary>
		/// <param name="user">The user<see cref="User"/>.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		Task UpdateSesionDataUser(User user);

		/// <summary>
		/// Compares the user asynchronous.
		/// </summary>
		/// <param name="user">The user<see cref="User"/>.</param>
		/// <returns>Task<see cref="Task{bool}" />.</returns>
		Task<bool> CompareUserAsync(User user);

		Task<User> GetUserByEmailAsync(string email);
	}
}