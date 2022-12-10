using SQLite;

namespace ProyectoXamarin.Models.ModelCar
{
	[Table("Model")]
	public class Model : BaseEntity
	{
		[MaxLength(50)]
		public string Name { get; set; }

		public int? IdBrand { get; set; }
	}
}