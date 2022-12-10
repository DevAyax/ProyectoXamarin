using System;
using System.IO;
using ProyectoXamarin.Interfaces;
using SQLite;

namespace ProyectoXamarin.iOS
{
	public class DatabaseConnection_iOS : IDataBaseConnection
	{
		public SQLiteAsyncConnection DbConnection()
		{
			var dbName = "MyDataBase.db";
			string personalFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string libraryFolder = Path.Combine(personalFolder);
			var path = Path.Combine(libraryFolder, dbName);
			return new SQLiteAsyncConnection(path);
		}
	}
}