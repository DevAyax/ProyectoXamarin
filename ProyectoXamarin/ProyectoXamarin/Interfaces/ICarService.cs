using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProyectoXamarin.Models.Brands;
using ProyectoXamarin.Models.Cars;
using ProyectoXamarin.Models.ModelCar;
using ProyectoXamarin.Models.Users;

namespace ProyectoXamarin.Interfaces
{
    public interface ICarService : IService<Car>
	{
		Task<ObservableCollection<Brand>> GetBrandsAsync(ObservableCollection<Brand> _brands);
		Task<Brand> GetBrandAsync(int brandId);
		Task<ObservableCollection<Model>> GetModelsAsync(ObservableCollection<Model> _models, int brandId);
		Task<Model> GetModelAsync(int modelId);
		Task<ObservableCollection<Car>> GetAllCarsAsync(ObservableCollection<Car> cars);
		Task UpdateAsync(Car entity);
		Task<Car> GetCarByUserAsync(int id);
	}
}