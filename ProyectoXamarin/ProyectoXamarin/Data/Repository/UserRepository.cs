// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-13-2022
// ***********************************************************************
// <copyright file="UserRepository.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Data.Repository;
using ProyectoXamarin.Models.Users;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserRepository))]

namespace ProyectoXamarin.Data.Repository
{
	/// <summary>
	/// Defines the <see cref="IUserRepository" />.
	/// </summary>
	public class UserRepository : IUserRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository"/> class.
		/// </summary>
		public UserRepository()
		{
		}

		/// <summary>
		/// Get by identifier as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A Task&<see cref="Task{ProyectoXamarin.Models.Users.User}"/> representing the asynchronous operation.</returns>
		public async Task<User> GetByIdAsync(int id)
		{
			var User = await App.DataBase.connect.Table<User>().Where(b => b.Id == id).FirstOrDefaultAsync();
			await App.DataBase.connect.CloseAsync();
			return User;
		}

		/// <summary>
		/// Get all as an asynchronous operation.
		/// </summary>
		/// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
		/// <returns>A Task<see cref="Task{List{ProyectoXamarin.Models.Users.User}}"/>.</returns>
		public async Task<List<User>> GetAllAsync(bool forceRefresh = false)
		{
			var users = await App.DataBase.connect.Table<User>().ToListAsync();
			var user = users.OrderByDescending(c => c.Id).FirstOrDefault();
			return users;
		}

		/// <summary>
		/// Delete user as an asynchronous operation.
		/// </summary>
		/// <param name="id">The id<see cref="int" />.</param>
		/// <returns>A Task<see cref="Task{int}"/>.</returns>
		public async Task<int> DeleteUserAsync(int id)
		{
			var User = await App.DataBase.connect.Table<User>().Where(b => b.Id == id).FirstOrDefaultAsync();
			return await App.DataBase.connect.DeleteAsync(User);
		}

		/// <summary>
		/// Save item as an asynchronous operation.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>A Task<see cref="{Task{int}"/>.</returns>
		public async Task<int> SaveItemAsync(User user)
		{
			var status = await App.DataBase.connect.InsertAsync(user);

			if (status == 1)
				await UpdateSesionDataUser(user);

			return status;
		}

		/// <summary>
		/// Compare user as an asynchronous operation.
		/// </summary>
		/// <param name="user">The user<see cref="User" />.</param>
		/// <returns>A Task<see cref="Task{bool}"/> representing the asynchronous operation.</returns>
		public async Task<bool> CompareUserAsync(User user)
		{
			var users = await App.DataBase.connect.Table<User>().ToListAsync();
			var isExist = users.Any(u => u.Email == user.Email);
			var passIsOk = users.Any(u => u.Email == user.Email & u.Password == user.Password);

			if (isExist & !passIsOk)
				UserDialogs.Instance.Alert("El password no coincide", "Error", "OK");

			bool isOk = isExist && passIsOk;

			return isOk;
		}

		/// <summary>
		/// Updates the sesion data user.
		/// </summary>
		/// <param name="user">The user<see cref="User" />.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		public async Task UpdateSesionDataUser(User user)
		{
			var newUser = await App.DataBase.connect.Table<User>().FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
			SesionData.UserId = newUser.Id;
		}

		/// <summary>
		/// Update item as an asynchronous operation.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>A Task<see cref="Task{int}"/> representing the asynchronous operation.</returns>
		public async Task<int> UpdateItemAsync(User item)
		{
			var status = await App.DataBase.connect.UpdateAsync(item);

			await UpdateSesionDataUser(item);

			return status;
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await App.DataBase.connect.Table<User>().FirstOrDefaultAsync(u => u.Email == email);
		}
	}
}