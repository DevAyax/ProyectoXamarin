// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-10-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-15-2022
// ***********************************************************************
// <copyright file="SesionData.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Xamarin.Essentials;

namespace ProyectoXamarin
{
	/// <summary>
	/// Class SesionData.
	/// </summary>
	public class SesionData
	{
		/// <summary>
		/// The session key
		/// </summary>
		public const string SessionKey = "F0E05BE7-A8D7-4EB5-8E32-54A05EA49C31";
		/// <summary>
		/// The email
		/// </summary>
		public const string Email = "D785E3C0-AEF6-41FF-BE6F-B6ABEA630D5B";

		/// <summary>
		/// The name
		/// </summary>
		public const string Name = "55AB9310-F6CB-463A-821E-3FCC47DFB9BA";

		/// <summary>
		/// The surnames
		/// </summary>
		public const string Surnames = "0E552D8C-9D93-48FD-B7C1-9CD7405B43AC";

		/// <summary>
		/// The password
		/// </summary>
		public const string Password = "59C57317-BB97-4DB8-A55E-3F66A59FC81F";

		/// <summary>
		/// The user identifier
		/// </summary>
		public static int UserId = 0;

		/// <summary>
		/// The car identifier
		/// </summary>
		public static int CarId = 0;

		/// <summary>
		/// The kilometers
		/// </summary>
		public static int Kilometers = 0;

		

		/// <summary>
		/// The country name
		/// </summary>
		public static string CountryName = string.Empty;

		/// <summary>
		/// The admin area
		/// </summary>
		public static string AdminArea = string.Empty;

		/// <summary>
		/// The locality
		/// </summary>
		public static string Locality = string.Empty;

		/// <summary>
		/// The address
		/// </summary>
		public static string Address = string.Empty;

		/// <summary>
		/// The latitude
		/// </summary>
		public static double latitude = 0;

		/// <summary>
		/// The longitude
		/// </summary>
		public static double longitude = 0;

		/// <summary>
		/// The placemark
		/// </summary>
		public static Placemark Placemark = null;

		/// <summary>
		/// The URL
		/// </summary>
		public static Uri Url;
	}
}