using SQLite;

namespace ProyectoXamarin.Models.Kilometers
{
    [Table("Kilometer")]
    public class Kilometer : BaseEntity
    {
        public string DateCreation { get; set; }
        public int? Km { get; set; }
        public int? CarId { get; set; }
    }
}
