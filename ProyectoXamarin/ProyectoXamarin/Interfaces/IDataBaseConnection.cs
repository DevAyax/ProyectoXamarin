using SQLite;

namespace ProyectoXamarin.Interfaces
{
	public interface IDataBaseConnection
	{
		SQLiteAsyncConnection DbConnection();
	}
}