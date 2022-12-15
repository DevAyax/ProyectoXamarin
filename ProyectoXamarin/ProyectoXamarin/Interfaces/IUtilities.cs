// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="IUtilities.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Interfaces
{
	using System.Threading.Tasks;

	/// <summary>
	/// Interface IUtilities
	/// </summary>
	public interface IUtilities
	{
		/// <summary>
		/// Gets the status.
		/// </summary>
		/// <param name="state">The state.</param>
		/// <param name="toast">The toast.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		Task GetStatus(int state, string toast);
	}
}