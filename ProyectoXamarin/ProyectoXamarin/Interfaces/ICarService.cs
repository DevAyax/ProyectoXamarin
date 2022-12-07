using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProyectoXamarin.Models.Brands;
using ProyectoXamarin.Models.Cars;
using ProyectoXamarin.Models.ModelCar;

namespace ProyectoXamarin.Interfaces
{
	public interface ICarService : IService<Car>
	{
		Task<ObservableCollection<Car>> GetAllCarsAsync(ObservableCollection<Car> _cars);

		Task<ObservableCollection<Model>> GetAllModelsAsync(ObservableCollection<Model> _models, int brandId);

		Task<ObservableCollection<Brand>> GetAllBrandsAsync(ObservableCollection<Brand> _brands);

		Task<Car> GetCarByUserAsync(int id);

		Task<Brand> GetBrandAsync(int brandId);

		Task<Model> GetModelAsync(int modelId);
	}
}