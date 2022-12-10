using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;

namespace ProyectoXamarin.Models.ModelCar
{
	public interface IRepositoryModel : IRepository<Model>
	{
		Task<List<Model>> GetByBrandIdAsync(int brandId);
		Task SetModelsIntoDataBase();
	}
}