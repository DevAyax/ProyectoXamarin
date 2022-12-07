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
		public SQLiteAsyncConnection db;

		public static string DbPath { get; } =
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)
				, "MyDataBase.db");

		public SQLiteAsyncConnection DbConnection()
		{
			db = new SQLiteAsyncConnection(DbPath);
			return db;
		}

		public DataBase(string dbPath)
		{
			db = new SQLiteAsyncConnection(DbPath);
			db.CreateTableAsync<User>().Wait();
			db.CreateTableAsync<Car>().Wait();
			db.CreateTableAsync<Brand>().Wait();
			db.CreateTableAsync<Model>().Wait();
			db.CreateTableAsync<Kilometer>().Wait();
		}
	}
}