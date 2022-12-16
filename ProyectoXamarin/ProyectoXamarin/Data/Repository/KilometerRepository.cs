// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-05-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="KilometerRepository.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Data.Repository;
using ProyectoXamarin.Models.Kilometers;
using Xamarin.Forms;

[assembly: Dependency(typeof(KilometerRepository))]

namespace ProyectoXamarin.Data.Repository
{
	/// <summary>
	/// Class KilometerRepository.
	/// Implements the <see cref="IKilometerRepository" />
	/// </summary>
	/// <seealso cref="IKilometerRepository" />
	public class KilometerRepository : IKilometerRepository
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="KilometerRepository"/> class.
		/// </summary>
		public KilometerRepository()
		{
		}

		/// <summary>
		/// Get all as an asynchronous operation.
		/// </summary>
		/// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
		/// <returns>A Task<see cref="Task{List{ProyectoXamarin.Models.Kilometers.Kilometer}"/> representing the asynchronous operation.</returns>
		public async Task<List<Kilometer>> GetAllAsync(bool forceRefresh = false)
		{
			return await App.DataBase.connect.Table<Kilometer>().OrderBy(k => k.DateCreation).ToListAsync();
		}

		/// <summary>
		/// Get as an asynchronous operation.
		/// </summary>
		/// <param name="km">The km.</param>
		/// <returns>A Task<see cref="Task{ProyectoXamarin.Models.Kilometers.Kilometer}"/> representing the asynchronous operation.</returns>
		public async Task<Kilometer> GetAsync(Kilometer km)
		{
			return await App.DataBase.connect.Table<Kilometer>().FirstOrDefaultAsync(k => k.DateCreation == km.DateCreation & k.Km == km.Km);
		}

		/// <summary>
		/// Get by identifier as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A Task<see cref="Task{ProyectoXamarin.Models.Kilometers.Kilometer}"/> representing the asynchronous operation.</returns>
		public async Task<Kilometer> GetByIdAsync(int id)
		{
			return await App.DataBase.connect.Table<Kilometer>().FirstOrDefaultAsync(k => k.Id == id);
		}

		/// <summary>
		/// Gets the by car identifier.
		/// </summary>
		/// <param name="carId">The car identifier.</param>
		/// <returns>Task <see cref="Task" />.</returns>
		public async Task<List<Kilometer>> GetByCarId(int carId)
		{
			return await App.DataBase.connect.Table<Kilometer>().Where(k => k.CarId == carId).ToListAsync();
		}

		/// <summary>
		/// Save as an asynchronous operation.
		/// </summary>
		/// <param name="kilometer">The id<see cref="Kilometer" />.</param>
		/// <returns>A Task<see cref="Task{int}"/> representing the asynchronous operation.</returns>
		public async Task<int> SaveAsync(Kilometer kilometer)
		{
			int status = 0;

			status = await App.DataBase.connect.InsertAsync(kilometer);
			await UpdateSesionDataKilometers(status, kilometer);

			return status;
		}

		/// <summary>
		/// Defines the <see cref="UpdateSesionDataKilometers" />.
		/// </summary>
		/// <param name="status">The id<see cref="int" />.</param>
		/// <param name="kilometer">The kilometer<see cref="Kilometer" />.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		public async Task UpdateSesionDataKilometers(int status, Kilometer kilometer)
		{
			if (status == 1)
			{
				var _kilometer = await App.DataBase.connect.Table<Kilometer>().Where(k => k.Km == kilometer.Km).FirstOrDefaultAsync();
				SesionData.Kilometers = (int) _kilometer.Km;
			}
		}
	}
}