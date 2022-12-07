using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;

namespace ProyectoXamarin.Models.Cars
{
	public interface ICarRepository : IRepository<Car>
	{
		Task<int> DeleteCarAsync(int id);

		Task<int> SaveCarAsync(Car car);

		Task<int> UpdateCarAsync(Car car);

		Task<Car> GetCarByUserAsync(int buserId);
	}
}