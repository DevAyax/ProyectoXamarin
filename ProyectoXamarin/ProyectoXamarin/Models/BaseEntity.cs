using SQLite;

namespace ProyectoXamarin.Models
{
	public class BaseEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
	}
}