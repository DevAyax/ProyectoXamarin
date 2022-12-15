// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-05-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="IKilometerService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Interfaces
{
	using System.Collections.ObjectModel;
	using System.Threading.Tasks;
	using ProyectoXamarin.Models.Kilometers;

	/// <summary>
	/// Interface IKilometerService
	/// Extends the <see cref="ProyectoXamarin.Interfaces.IService{ProyectoXamarin.Models.Kilometers.Kilometer}" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Interfaces.IService{ProyectoXamarin.Models.Kilometers.Kilometer}" />
	public interface IKilometerService : IService<Kilometer>
	{
		/// <summary>
		/// Gets all kilometers asynchronous.
		/// </summary>
		/// <param name="kilometers">The kilometers.</param>
		/// <returns>Task <see cref="{Task{ObservableCollection{ProyectoXamarin.Models.Kilometers.Kilometer}"/>.</returns>
		Task<ObservableCollection<Kilometer>> GetAllKilometersAsync(ObservableCollection<Kilometer> kilometers);

		/// <summary>
		/// Gets the kilometer asynchronous.
		/// </summary>
		/// <param name="carId">The car identifier.</param>
		/// <returns>Task <see cref="Task{ProyectoXamarin.Models.Kilometers.Kilometer}"/>.</returns>
		Task<Kilometer> GetKilometerAsync(int carId);
	}
}