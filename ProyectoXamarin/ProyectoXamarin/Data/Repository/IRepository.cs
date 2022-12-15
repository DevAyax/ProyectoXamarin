// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-10-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="IRepository.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Data.Repository
{
	/// <summary>
	/// Defines the <see cref="IRepository{T}" />.
	/// </summary>
	/// <typeparam name="T">Repository type to be created.</typeparam>
	public interface IRepository<T> where T : class
	{
	}
}