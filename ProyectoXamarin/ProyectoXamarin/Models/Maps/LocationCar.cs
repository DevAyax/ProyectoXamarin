// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-10-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="LocationCar.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Models.Maps
{
	using SQLite;

	/// <summary>
	/// Class LocationCar.
	/// Implements the <see cref="ProyectoXamarin.Models.BaseEntity" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Models.BaseEntity" />
	[Table("LocationCar")]
	public class LocationCar : BaseEntity
	{
		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		public double? Latitude {get; set;}

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		public double? Longitude { get; set; }

		/// <summary>
		/// Gets or sets the name of the country.
		/// </summary>
		/// <value>The name of the country.</value>
		[MaxLength(50)]
		public string CountryName {get; set;}

		/// <summary>
		/// Gets or sets the admin area.
		/// </summary>
		/// <value>The admin area.</value>
		[MaxLength(20)]
		public string AdminArea { get; set;}

		/// <summary>
		/// Gets or sets the locality.
		/// </summary>
		/// <value>The locality.</value>
		[MaxLength(150)]
		public string Locality { get; set;}

		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		[MaxLength(200)]
		public string Address {get; set;}

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>The user identifier.</value>
		public int? UserId { get; set; }
	}
}