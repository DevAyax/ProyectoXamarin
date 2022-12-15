// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-27-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="Car.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Models.Cars
{
	using SQLite;

	/// <summary>
	/// Class Car.
	/// Implements the <see cref="ProyectoXamarin.Models.BaseEntity" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Models.BaseEntity" />
	[Table("Car")]
	public class Car : BaseEntity
	{
		/// <summary>
		/// Gets or sets the brand identifier.
		/// </summary>
		/// <value>The brand identifier.</value>
		public int? BrandId { get; set; }
		/// <summary>
		/// Gets or sets the model identifier.
		/// </summary>
		/// <value>The model identifier.</value>
		public int? ModelId { get; set; }
		/// <summary>
		/// Gets or sets the doors.
		/// </summary>
		/// <value>The doors.</value>
		public string Doors { get; set; }
		/// <summary>
		/// Gets or sets the combustible.
		/// </summary>
		/// <value>The combustible.</value>
		public string Combustible { get; set; }
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>The user identifier.</value>
		public int UserId { get; set; }
		/// <summary>
		/// Gets or sets the location identifier.
		/// </summary>
		/// <value>The location identifier.</value>
		public int? LocationId { get; set; }
		/// <summary>
		/// Gets or sets the km.
		/// </summary>
		/// <value>The km.</value>
		public int? Km { get; set; }
	}
}