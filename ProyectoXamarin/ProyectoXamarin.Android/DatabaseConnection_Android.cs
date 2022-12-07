using System.IO;
using Android.Database.Sqlite;
using ProyectoXamarin.Droid;
using ProyectoXamarin.Interfaces;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]

namespace ProyectoXamarin.Droid
{
	public class DatabaseConnection_Android : IDataBaseConnection
	{
		public SQLiteAsyncConnection DbConnection()
		{
			var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MyDataBase.db");
			SQLiteDatabase.DeleteDatabase(new Java.IO.File(path));
			return new SQLiteAsyncConnection(path);
		}
	}
}