using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;

namespace ProyectoXamarin.Models.Kilometers
{
	public interface IKilometerRepository : IRepository<Kilometer>
	{
		Task<int> SaveAsync(Kilometer kilometer);

		Task<Kilometer> GetAsync(Kilometer km);

		Task UpdateSesionDataKilometers(int status, Kilometer kilometer);

		Task<List<Kilometer>> GetByCarId(int carId);
	}
}