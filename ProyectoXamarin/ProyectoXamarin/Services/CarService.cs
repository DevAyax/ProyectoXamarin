// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="CarService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ProyectoXamarin.Enums;
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
	/// <summary>
	/// Class CarService.
	/// Implements the <see cref="ICarService" />
	/// </summary>
	/// <seealso cref="ICarService" />
	public class CarService : ICarService
	{
		/// <summary>
		/// The car repository
		/// </summary>
		private readonly ICarRepository carRepository;

		/// <summary>
		/// The utilities
		/// </summary>
		private readonly IUtilities utilities;

		/// <summary>
		/// The user repository
		/// </summary>
		private readonly IUserRepository userRepository;

		/// <summary>
		/// The brand repository
		/// </summary>
		private readonly IBrandRepository brandRepository;

		/// <summary>
		/// The model repository
		/// </summary>
		private readonly IRepositoryModel modelRepository;

		/// <summary>
		/// The user service
		/// </summary>
		private readonly IUserService userService;

		/// <summary>
		/// Initializes a new instance of the <see cref="CarService"/> class.
		/// </summary>
		public CarService()
		{
			this.carRepository = DependencyService.Get<ICarRepository>();
			this.userRepository = DependencyService.Get<IUserRepository>();
			this.brandRepository = DependencyService.Get<IBrandRepository>();
			this.modelRepository = DependencyService.Get<IRepositoryModel>();
			this.utilities = DependencyService.Get<IUtilities>();
			this.userService = DependencyService.Get<IUserService>();
		}

		/// <summary>
		/// Get all brands as an asynchronous operation.
		/// </summary>
		/// <param name="_brands">The brands.</param>
		/// <returns>A Task&lt;ObservableCollection`1&gt; representing the asynchronous operation.</returns>
		public async Task<ObservableCollection<Brand>> GetAllBrandsAsync(ObservableCollection<Brand> _brands)
		{
			var brands = await brandRepository.GetAllAsync();

			if (_brands.Count() != 0)
				_brands.Clear();

			foreach (var brand in brands)
				_brands.Add(brand);

			return _brands;
		}

		/// <summary>
		/// Get all models as an asynchronous operation.
		/// </summary>
		/// <param name="_models">The models.</param>
		/// <param name="brandId">The brand identifier.</param>
		/// <returns>A Task&lt;ObservableCollection`1&gt; representing the asynchronous operation.</returns>
		public async Task<ObservableCollection<Model>> GetAllModelsAsync(ObservableCollection<Model> _models, int brandId)
		{
			var models = await modelRepository.GetByBrandIdAsync(brandId);

			if (_models.Count() != 0)
				_models.Clear();

			foreach (var model in models)
				_models.Add(model);

			return _models;
		}

		/// <summary>
		/// Get all cars as an asynchronous operation.
		/// </summary>
		/// <param name="_cars">The cars.</param>
		/// <returns>A Task<see cref="Task{ObservableCollection{ProyectoXamarin.Models.Cars.Car}}"/>representing the asynchronous operation.</returns>
		public async Task<ObservableCollection<Car>> GetAllCarsAsync(ObservableCollection<Car> _cars)
		{
			var cars = await carRepository.GetAllAsync();

			if (_cars.Count() != 0)
				_cars.Clear();

			foreach (var car in cars)
				_cars.Add(car);

			return _cars;
		}

		/// <summary>
		/// Save as an asynchronous operation.
		/// </summary>
		/// <param name="car">The car.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task SaveAsync(Car car)
		{
			var status = await carRepository.SaveCarAsync(car);

			if (status == 1)
			{
				var user = await userService.GetUserAsync(SesionData.UserId);
				user.CarId = SesionData.CarId;
				await userService.UpdateAsync(user);
			}

			status = status == 1 ? (int) StatusEnum.Registered : (int) StatusEnum.ErrorInProcess;
			await utilities.GetStatus(status, nameof(EntityEnums.Car));

			await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
		}

		/// <summary>
		/// Get car by user as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A Task&lt;Car&gt; representing the asynchronous operation.</returns>
		public async Task<Car> GetCarByUserAsync(int id)
		{
			return await carRepository.GetCarByUserAsync(id);
		}

		/// <summary>
		/// Get brand as an asynchronous operation.
		/// </summary>
		/// <param name="brandId">The brand identifier.</param>
		/// <returns>A Task&lt;Brand&gt; representing the asynchronous operation.</returns>
		public async Task<Brand> GetBrandAsync(int brandId)
		{
			return await brandRepository.GetByIdAsync(brandId);
		}

		/// <summary>
		/// Get model as an asynchronous operation.
		/// </summary>
		/// <param name="modelId">The model identifier.</param>
		/// <returns>A Task&lt;Model&gt; representing the asynchronous operation.</returns>
		public async Task<Model> GetModelAsync(int modelId)
		{
			return await modelRepository.GetByIdAsync(modelId);
		}

		/// <summary>
		/// Update as an asynchronous operation.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task UpdateAsync(Car entity)
		{
			var status = await carRepository.UpdateCarAsync(entity);

			status = status == 1 ? (int) StatusEnum.Updated : (int) StatusEnum.ErrorInProcess;

			await utilities.GetStatus(status, nameof(EntityEnums.Car));

			await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
		}

		/// <summary>
		/// Sets the data into data base.
		/// </summary>
		/// <returns>Task<see cref="Task" />.</returns>
		public async Task SetDataIntoDataBase()
		{
			await brandRepository.SetBrandsIntoDataBase();
			await modelRepository.SetModelsIntoDataBase();
		}

		/// <summary>
		/// Delete car as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task DeleteCarAsync(int id)
		{
			if (id != 0)
			{
				var status = await carRepository.DeleteAsync(id);
				status = status == 1 ? (int) StatusEnum.Deleted : (int) StatusEnum.ErrorInProcess;
				await utilities.GetStatus(status, nameof(EntityEnums.Car));
			}
		}
	}
}