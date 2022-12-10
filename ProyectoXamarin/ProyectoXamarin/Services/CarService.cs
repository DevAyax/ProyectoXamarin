using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Brands;
using ProyectoXamarin.Models.Cars;
using ProyectoXamarin.Models.ModelCar;
using ProyectoXamarin.Models.Users;
using ProyectoXamarin.Services;
using ProyectoXamarin.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(CarService))]

namespace ProyectoXamarin.Services
{
	public class CarService : ICarService
	{
		private readonly ICarRepository carRepository;
		private readonly IUtilities utilities;
		private readonly IUserRepository userRepository;
		private readonly IBrandRepository brandRepository;
		private readonly IRepositoryModel modelRepository;
		private readonly IUserService userService;

		public CarService()
		{
			this.carRepository = DependencyService.Get<ICarRepository>();
			this.userRepository = DependencyService.Get<IUserRepository>();
			this.brandRepository = DependencyService.Get<IBrandRepository>();
			this.modelRepository = DependencyService.Get<IRepositoryModel>();
			this.utilities = DependencyService.Get<IUtilities>();
			this.userService = DependencyService.Get<IUserService>();
		}

		public async Task<ObservableCollection<Brand>> GetAllBrandsAsync(ObservableCollection<Brand> _brands)
		{
			var brands = await brandRepository.GetAllAsync();

			if (_brands.Count() != 0)
				_brands.Clear();

			foreach (var brand in brands)
				_brands.Add(brand);

			return _brands;
		}

		public async Task<ObservableCollection<Model>> GetAllModelsAsync(ObservableCollection<Model> _models, int brandId)
		{
			var models = await modelRepository.GetByBrandIdAsync(brandId);

			if (_models.Count() != 0)
				_models.Clear();

			foreach (var model in models)
				_models.Add(model);

			return _models;
		}

		public async Task<ObservableCollection<Car>> GetAllCarsAsync(ObservableCollection<Car> _cars)
		{
			var cars = await carRepository.GetAllAsync();

			if (_cars.Count() != 0)
				_cars.Clear();

			foreach (var car in cars)
				_cars.Add(car);

			return _cars;
		}

		public async Task SaveAsync(Car car)
		{
			var statusCar = await carRepository.SaveCarAsync(car);

			await utilities.GetSatatus(statusCar, "Coche");

			if (statusCar == 1)
			{
				var user = await userService.GetUserAsync(SesionData.UserId);
				user.CarId = SesionData.CarId;
				await userService.SaveAsync(user);
			}

			await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
		}

		public async Task<Car> GetCarByUserAsync(int id)
		{
			return await carRepository.GetCarByUserAsync(id);
		}

		public async Task<Brand> GetBrandAsync(int brandId)
		{
			return await brandRepository.GetByIdAsync(brandId);
		}

		public async Task<Model> GetModelAsync(int modelId)
		{
			return await modelRepository.GetByIdAsync(modelId);
		}

		public async Task UpdateAsync(Car entity)
		{
			var status = await carRepository.UpdateCarAsync(entity);
			await utilities.GetSatatus(status, "Coche");
			await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
		}

		public async Task SetDataIntoDataBase()
		{
			await brandRepository.SetBrandsIntoDataBase();
			await modelRepository.SetModelsIntoDataBase();
			var brands = await brandRepository.GetAllAsync();
			var models = await modelRepository.GetAllAsync();
		}
	}
}