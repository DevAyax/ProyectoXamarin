// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-27-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="ICustomDependencyService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Intetrfaces
{
	/// <summary>
	/// Interface ICustomDependencyService
	/// </summary>
	public interface ICustomDependencyService
	{
		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>T.</returns>
		T Get<T>() where T : class;
	}
}