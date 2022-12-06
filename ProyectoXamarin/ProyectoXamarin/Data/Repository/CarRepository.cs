﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using ProyectoXamarin.Models.Cars;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Data.Repository;

[assembly: Dependency(typeof(CarRepository))]
namespace ProyectoXamarin.Data.Repository
{
    public class CarRepository : ICarRepository
    {
        //SQLiteAsyncConnection db;

        public CarRepository()
        {
        }

		//public async Task InitAsync()
		//{
		//	if (db != null)
		//	{
		//		return;
		//	}

		//	var dataBase = DependencyService.Get<IDataBaseConnection>();
		//	db = dataBase.DbConnection();
		//	await App.DataBase.db.CreateTableAsync<Car>();
		//}

		public async Task<Car> GetByIdAsync(int id)
        {
            var Car = await App.DataBase.db.Table<Car>().Where(b => b.Id == id).FirstOrDefaultAsync();
            await App.DataBase.db.CloseAsync();
            return Car;
        }

        public async Task<List<Car>> GetAllAsync(bool forceRefresh = false)
        {
            var Cars = await App.DataBase.db.Table<Car>().ToListAsync();
            await App.DataBase.db.CloseAsync();
            return Cars;
        }


		public async Task<int> UpdateCarAsync(Car car)
		{
			var status = await App.DataBase.db.UpdateAsync(car);

			if (status != 0)
			{
				await AddConstantCar(status, car);
				status++;
			}

			return status;
		}

		public async Task<int> SaveCarAsync(Car car)
		{
			var status = await App.DataBase.db.InsertAsync(car);
			await AddConstantCar(status, car);
				
			return status;
		}

		public async Task<int> DeleteCarAsync(int id)
		{
			var car = await App.DataBase.db.Table<Car>().Where(b => b.Id == id).FirstOrDefaultAsync();
			return await App.DataBase.db.DeleteAsync(car);
		}

		public async Task<Car> GetCarByUserAsync(int userId)
		{
			var car = await App.DataBase.db.Table<Car>().Where(c => c.UserId == userId).OrderByDescending(c => c.Id).FirstOrDefaultAsync();
			return car;
		}

		public async Task AddConstantCar(int status, Car car)
		{
			if (status == 1)
			{
				var newCar = await App.DataBase.db.Table<Car>().Where(c => c.BrandId == car.BrandId & c.ModelId == car.ModelId).FirstOrDefaultAsync();
				Constants.CarId = newCar.Id;
			}
		}

	}
}
