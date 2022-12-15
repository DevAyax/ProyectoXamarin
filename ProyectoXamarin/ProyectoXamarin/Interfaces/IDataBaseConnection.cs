// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="IDataBaseConnection.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Interfaces
{
	using SQLite;

	/// <summary>
	/// Interface IDataBaseConnection
	/// </summary>
	public interface IDataBaseConnection
	{
		/// <summary>
		/// Databases the connection.
		/// </summary>
		/// <returns>SQLiteAsyncConnection.</returns>
		SQLiteAsyncConnection DbConnection();
	}
}