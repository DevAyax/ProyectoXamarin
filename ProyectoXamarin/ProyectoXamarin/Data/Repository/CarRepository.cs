// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="CarRepository.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Data.Repository;
using ProyectoXamarin.Models.Cars;
using Xamarin.Forms;

[assembly: Dependency(typeof(CarRepository))]

namespace ProyectoXamarin.Data.Repository
{
	/// <summary>
	/// Class CarRepository.
	/// Implements the <see cref="ICarRepository" />
	/// </summary>
	/// <seealso cref="ICarRepository" />
	public class CarRepository : ICarRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CarRepository"/> class.
		/// </summary>
		public CarRepository()
		{
		}

		/// <summary>
		/// Get by identifier as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A Task<see cref="Task{ProyectoXamarin.Models.Cars.Car}"/>; representing the asynchronous operation.</returns>
		public async Task<Car> GetByIdAsync(int id)
		{
			var Car = await App.DataBase.connect.Table<Car>().Where(b => b.Id == id).FirstOrDefaultAsync();

			return Car;
		}

		/// <summary>
		/// Get all as an asynchronous operation.
		/// </summary>
		/// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
		/// <returns>A Task<see cref="Task{List{ProyectoXamarin.Models.Cars.Car}}"/> representing the asynchronous operation.</returns>
		public async Task<List<Car>> GetAllAsync(bool forceRefresh = false)
		{
			var Cars = await App.DataBase.connect.Table<Car>().ToListAsync();

			return Cars;
		}

		/// <summary>
		/// Update car as an asynchronous operation.
		/// </summary>
		/// <param name="car">The id<see cref="Car" />.</param>
		/// <returns>A Task<see cref="Task{int}"/> representing the asynchronous operation.</returns>
		public async Task<int> UpdateCarAsync(Car car)
		{
			var status = await App.DataBase.connect.UpdateAsync(car);

			await UpdateSesionDataCar(status, car);

			return status;
		}

		/// <summary>
		/// Save car as an asynchronous operation.
		/// </summary>
		/// <param name="car">The car object<see cref="Car" />.</param>
		/// <returns>A Task<see cref="Task{int}"/> representing the asynchronous operation.</returns>
		public async Task<int> SaveCarAsync(Car car)
		{
			var status = await App.DataBase.connect.InsertAsync(car);
			await UpdateSesionDataCar(status, car);

			return status;
		}

		/// <summary>
		/// Delete as an asynchronous operation.
		/// </summary>
		/// <param name="id">The id<see cref="int" />.</param>
		/// <returns>A Task<see cref="Task{int}"/> representing the asynchronous operation.</returns>
		public async Task<int> DeleteAsync(int id)
		{
			var car = await App.DataBase.connect.Table<Car>().Where(b => b.Id == id).FirstOrDefaultAsync();
			return await App.DataBase.connect.DeleteAsync(car);
		}

		/// <summary>
		/// Get car by user as an asynchronous operation.
		/// </summary>
		/// <param name="userId">The id<see cref="int" />.</param>
		/// <returns>A Task<see cref="Task{List{ProyectoXamarin.Models.Cars.Car}}"/> representing the asynchronous operation.</returns>
		public async Task<Car> GetCarByUserAsync(int userId)
		{
			var car = await App.DataBase.connect.Table<Car>().Where(c => c.UserId == userId).OrderByDescending(c => c.Id).FirstOrDefaultAsync();

			return car;
		}

		/// <summary>
		/// Updates the sesion data car.
		/// </summary>
		/// <param name="status">The status.</param>
		/// <param name="car">The car.</param>
		/// <returns>Task<see cref="Task" />.</returns>
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