// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-12-2022
// ***********************************************************************
// <copyright file="IUserService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Interfaces
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using ProyectoXamarin.Models.Users;

	/// <summary>
	/// Interface IUserService
	/// Extends the <see cref="ProyectoXamarin.Interfaces.IService{ProyectoXamarin.Models.Users.User}" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Interfaces.IService{ProyectoXamarin.Models.Users.User}" />
	public interface IUserService : IService<User>
	{
		/// <summary>
		/// Exists the asynchronous.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>Task<see cref="Task{bool}"/>.</returns>
		Task<bool> ExistAsync(User entity);

		/// <summary>
		/// Gets all users asynchronous.
		/// </summary>
		/// <returns>The Task<see cref="Task{ProyectoXamarin.Models.Users.User}"/>.</returns>
		Task<List<User>> GetAllUsersAsync();

		/// <summary>
		/// Gets the user asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Task<see cref="Task{ProyectoXamarin.Models.Users.User}"/>.</returns>
		Task<User> GetUserAsync(int id);

		/// <summary>
		/// Initializes the asynchronous.
		/// </summary>
		/// <returns>Task<see cref="Task" />.</returns>
		Task<bool> InitAsync();

		/// <summary>
		/// Logins the user asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		Task LoginUserAsync(User user);

		/// <summary>
		/// Initializes the asynchronous.
		/// </summary>
		/// <returns>Task<see cref="Task" />.</returns>
		Task LogoutUserAsync();

		/// <summary>
		/// Initializes the asynchronous.
		/// </summary>
		/// <returns>Task<see cref="Task" />.</returns>
		Task AutoLoginAsync();
	}
}