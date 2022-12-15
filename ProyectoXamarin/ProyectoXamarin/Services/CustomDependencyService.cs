// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-27-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="CustomDependencyService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Services
{
	using ProyectoXamarin.Intetrfaces;
	using Xamarin.Forms;
	/// <summary>
	/// Class CustomDependencyService.
	/// Implements the <see cref="ICustomDependencyService" />
	/// </summary>
	/// <seealso cref="ICustomDependencyService" />
	public class CustomDependencyService : ICustomDependencyService
	{
		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>T.</returns>
		public T Get<T>() where T : class
		{
			return DependencyService.Get<T>();
		}
	}
}