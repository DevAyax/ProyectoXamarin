using SQLite;

namespace ProyectoXamarin.Models.Brands
{
	[Table("Brand")]
	public class Brand : BaseEntity
	{
		[MaxLength(50)]
		public string Name { get; set; }
	}
}