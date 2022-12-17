// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-28-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="DataBase.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using ProyectoXamarin.Data;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Brands;
using ProyectoXamarin.Models.Cars;
using ProyectoXamarin.Models.Kilometers;
using ProyectoXamarin.Models.Maps;
using ProyectoXamarin.Models.ModelCar;
using ProyectoXamarin.Models.Users;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataBase))]

namespace ProyectoXamarin.Data
{
	/// <summary>
	/// Class DataBase.
	/// Implements the <see cref="IDataBaseConnection" />
	/// </summary>
	/// <seealso cref="IDataBaseConnection" />
	public class DataBase : IDataBaseConnection
	{
		/// <summary>
		/// The connect
		/// </summary>
		public SQLiteAsyncConnection connect;

		/// <summary>
		/// Gets the database path.
		/// </summary>
		/// <value>The database path.</value>
		public static string DbPath { get; } =
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)
				, "MyDataBase.db");

		/// <summary>
		/// Databases the connection.
		/// </summary>
		/// <returns>SQLiteAsyncConnection<see cref="SQLiteAsyncConnection"/>.</returns>
		public SQLiteAsyncConnection DbConnection()
		{
			connect = new SQLiteAsyncConnection(DbPath);
			return connect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataBase"/> class.
		/// </summary>
		/// <param name="dbPath">The database path.</param>
		public DataBase(string dbPath)
		{
			connect = new SQLiteAsyncConnection(DbPath);
			connect.CreateTableAsync<User>().Wait();
			connect.CreateTableAsync<Car>().Wait();
			connect.CreateTableAsync<Brand>().Wait();
			connect.CreateTableAsync<Model>().Wait();
			connect.CreateTableAsync<Kilometer>().Wait();
			connect.CreateTableAsync<LocationCar>().Wait();
		}
	}
}