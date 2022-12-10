using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Data.Repository;
using ProyectoXamarin.Models.Cars;
using Xamarin.Forms;

[assembly: Dependency(typeof(CarRepository))]

namespace ProyectoXamarin.Data.Repository
{
	public class CarRepository : ICarRepository
	{
		public CarRepository()
		{
		}

		public async Task<Car> GetByIdAsync(int id)
		{
			var Car = await App.DataBase.connect.Table<Car>().Where(b => b.Id == id).FirstOrDefaultAsync();

			return Car;
		}

		public async Task<List<Car>> GetAllAsync(bool forceRefresh = false)
		{
			var Cars = await App.DataBase.connect.Table<Car>().ToListAsync();

			return Cars;
		}

		public async Task<int> UpdateCarAsync(Car car)
		{
			var status = await App.DataBase.connect.UpdateAsync(car);

			if (status != 0)
			{
				await UpdateSesionDataCar(status, car);
				status++;
			}

			return status;
		}

		public async Task<int> SaveCarAsync(Car car)
		{
			var status = await App.DataBase.connect.InsertAsync(car);
			await UpdateSesionDataCar(status, car);

			return status;
		}

		public async Task<int> DeleteCarAsync(int id)
		{
			var car = await App.DataBase.connect.Table<Car>().Where(b => b.Id == id).FirstOrDefaultAsync();
			return await App.DataBase.connect.DeleteAsync(car);
		}

		public async Task<Car> GetCarByUserAsync(int userId)
		{
			var car = await App.DataBase.connect.Table<Car>().Where(c => c.UserId == userId).OrderByDescending(c => c.Id).FirstOrDefaultAsync();

			return car;
		}

		public async Task UpdateSesionDataCar(int status, Car car)
		{
			if (status == 1)
			{
				var newCar = await App.DataBase.connect.Table<Car>().Where(c => c.BrandId == car.BrandId & c.ModelId == car.ModelId).FirstOrDefaultAsync();
				SesionData.CarId = newCar.Id;
			}
		}
	}
}