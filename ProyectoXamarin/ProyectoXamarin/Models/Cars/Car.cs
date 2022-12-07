using SQLite;

namespace ProyectoXamarin.Models.Cars
{
	[Table("Car")]
	public class Car : BaseEntity
	{
		public int? BrandId { get; set; }
		public int? ModelId { get; set; }
		public string Doors { get; set; }
		public string Combustible { get; set; }
		public int UserId { get; set; }
		public int? LocationId { get; set; }
		public int? Km { get; set; }
	}
}