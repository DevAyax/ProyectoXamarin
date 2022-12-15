// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-05-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="KilometerService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Kilometers;
using ProyectoXamarin.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(KilometerService))]

namespace ProyectoXamarin.Services
{
	/// <summary>
	/// Class KilometerService.
	/// Implements the <see cref="IKilometerService" />
	/// </summary>
	/// <seealso cref="IKilometerService" />
	public class KilometerService : IKilometerService
	{
		/// <summary>
		/// The kilometer repository
		/// </summary>
		private readonly IKilometerRepository kilometerRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="KilometerService"/> class.
		/// </summary>
		public KilometerService()
		{
			this.kilometerRepository = DependencyService.Get<IKilometerRepository>();
		}

		/// <summary>
		/// Get all kilometers as an asynchronous operation.
		/// </summary>
		/// <param name="kilometers">The kilometers.</param>
		/// <returns>Task <see cref="{Task{ObservableCollection{ProyectoXamarin.Models.Kilometers.Kilometer}"/>.</returns>
		public async Task<ObservableCollection<Kilometer>> GetAllKilometersAsync(ObservableCollection<Kilometer> kilometers)
		{
			var kms = await kilometerRepository.GetAllAsync();

			if (kilometers.Count() > 0)
				kilometers.Clear();

			foreach (var km in kms.OrderByDescending(k => k.Km).ToList())
			{
				kilometers.Add(km);
			}

			return kilometers;
		}

		/// <summary>
		/// Save as an asynchronous operation.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task SaveAsync(Kilometer entity)
		{
			var kilometer = await kilometerRepository.GetAsync(entity);

			if (kilometer == null)
			{
				var status = await kilometerRepository.SaveAsync(entity);

				if (status > 0)
				{
					await kilometerRepository.UpdateSesionDataKilometers(status, entity);
				}
			}
		}

		/// <summary>
		/// Get kilometer as an asynchronous operation.
		/// </summary>
		/// <param name="carId">The car identifier.</param>
		/// <returns>Task <see cref="Task{ProyectoXamarin.Models.Kilometers.Kilometer}"/>.</returns>
		public async Task<Kilometer> GetKilometerAsync(int carId)
		{
			var kms = await kilometerRepository.GetByCarId(carId);
			var km = kms.Where(k => k.CarId == carId).OrderByDescending(c => c.Id).FirstOrDefault();

			return km;
		}

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>Task<see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public Task UpdateAsync(Kilometer entity)
		{
			throw new System.NotImplementedException();
		}
	}
}