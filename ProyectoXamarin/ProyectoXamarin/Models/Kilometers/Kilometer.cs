// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-05-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="Kilometer.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace ProyectoXamarin.Models.Kilometers
{
	using SQLite;

	/// <summary>
	/// Class Kilometer.
	/// Implements the <see cref="ProyectoXamarin.Models.BaseEntity" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Models.BaseEntity" />
	[Table("Kilometer")]
	public class Kilometer : BaseEntity
	{
		/// <summary>
		/// Gets or sets the date creation.
		/// </summary>
		/// <value>The date creation.</value>
		public string DateCreation { get; set; }

		/// <summary>
		/// Gets or sets the km.
		/// </summary>
		/// <value>The km.</value>
		public int? Km { get; set; }

		/// <summary>
		/// Gets or sets the car identifier.
		/// </summary>
		/// <value>The car identifier.</value>
		public int? CarId { get; set; }
	}
}