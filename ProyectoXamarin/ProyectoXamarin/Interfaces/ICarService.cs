// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="ICarService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Interfaces
{
	using System.Collections.ObjectModel;
	using System.Threading.Tasks;
	using ProyectoXamarin.Models.Brands;
	using ProyectoXamarin.Models.Cars;
	using ProyectoXamarin.Models.ModelCar;

	/// <summary>
	/// Interface ICarService
	/// Extends the <see cref="ProyectoXamarin.Interfaces.IService{ProyectoXamarin.Models.Cars.Car}" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Interfaces.IService{ProyectoXamarin.Models.Cars.Car}" />
	public interface ICarService : IService<Car>
	{
		/// <summary>
		/// Gets all cars asynchronous.
		/// </summary>
		/// <param name="_cars">The cars.</param>
		/// <returns>A Task<see cref="Task{ObservableCollection{ProyectoXamarin.Models.Cars.Car}}"/>.</returns>
		Task<ObservableCollection<Car>> GetAllCarsAsync(ObservableCollection<Car> _cars);

		/// <summary>
		/// Gets all models asynchronous.
		/// </summary>
		/// <param name="_models">The models.</param>
		/// <param name="brandId">The brand identifier.</param>
		/// <returns>A Task<see cref="Task{ObservableCollection{ProyectoXamarin.Models.ModelCar.Model}}"/>.</returns>
		Task<ObservableCollection<Model>> GetAllModelsAsync(ObservableCollection<Model> _models, int brandId);

		/// <summary>
		/// Gets all brands asynchronous.
		/// </summary>
		/// <param name="_brands">The brands.</param>
		/// <returns>A Task<see cref="Task{ObservableCollection{ProyectoXamarin.Models.Brands.Brand}}"/>.</returns>
		Task<ObservableCollection<Brand>> GetAllBrandsAsync(ObservableCollection<Brand> _brands);

		/// <summary>
		/// Gets the car by user asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Task<see cref="Task{ProyectoXamarin.Models.Cars.Car}"/>.</returns>
		Task<Car> GetCarByUserAsync(int id);

		/// <summary>
		/// Gets the brand asynchronous.
		/// </summary>
		/// <param name="brandId">The brand identifier.</param>
		/// <returns>Task<see cref="Task{ProyectoXamarin.Models.Brands.Brand}"/>.</returns>
		Task<Brand> GetBrandAsync(int brandId);

		/// <summary>
		/// Gets the model asynchronous.
		/// </summary>
		/// <param name="modelId">The model identifier.</param>
		/// <returns>Task<see cref="Task{ProyectoXamarin.Models.ModelCar.Model}"/>.</returns>
		Task<Model> GetModelAsync(int modelId);

		/// <summary>
		/// Sets the data into data base.
		/// </summary>
		/// <returns>Task<see cref="Task" />.</returns>
		Task SetDataIntoDataBase();

		/// <summary>
		/// Deletes the car asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		Task DeleteCarAsync(int id);
	}
}