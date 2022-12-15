// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-27-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="Model.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace ProyectoXamarin.Models.ModelCar
{
	using SQLite;

	/// <summary>
	/// Class Model.
	/// Implements the <see cref="ProyectoXamarin.Models.BaseEntity" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Models.BaseEntity" />
	[Table("Model")]
	public class Model : BaseEntity
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[MaxLength(50)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the identifier brand.
		/// </summary>
		/// <value>The identifier brand.</value>
		public int? IdBrand { get; set; }
	}
}