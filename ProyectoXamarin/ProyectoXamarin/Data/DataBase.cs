using System;
using System.IO;
using ProyectoXamarin.Data;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Brands;
using ProyectoXamarin.Models.Cars;
using ProyectoXamarin.Models.Kilometers;
using ProyectoXamarin.Models.ModelCar;
using ProyectoXamarin.Models.Users;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataBase))]

namespace ProyectoXamarin.Data
{
	public class DataBase : IDataBaseConnection
	{
		public SQLiteAsyncConnection connect;

		public static string DbPath { get; } =
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)
				, "MyDataBase.db");

		public SQLiteAsyncConnection DbConnection()
		{
			connect = new SQLiteAsyncConnection(DbPath);
			return connect;
		}

		public DataBase(string dbPath)
		{
			connect = new SQLiteAsyncConnection(DbPath);
			connect.CreateTableAsync<User>().Wait();
			connect.CreateTableAsync<Car>().Wait();
			connect.CreateTableAsync<Brand>().Wait();
			connect.CreateTableAsync<Model>().Wait();
			connect.CreateTableAsync<Kilometer>().Wait();
		}
	}
}