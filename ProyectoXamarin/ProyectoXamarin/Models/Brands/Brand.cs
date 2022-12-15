// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-27-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="Brand.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace ProyectoXamarin.Models.Brands
{
	using SQLite;

	/// <summary>
	/// Class Brand.
	/// Implements the <see cref="ProyectoXamarin.Models.BaseEntity" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Models.BaseEntity" />
	[Table("Brand")]
	public class Brand : BaseEntity
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[MaxLength(50)]
		public string Name { get; set; }
	}
}