using System.IO;
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
			var dbName = "MyDataBase.db";
			var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
			return new SQLiteAsyncConnection(path);

		}
	}
}