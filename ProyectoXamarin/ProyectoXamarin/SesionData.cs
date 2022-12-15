// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-10-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-12-2022
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
		public const string SessionKey = "F0E05BE7-A8D7-4EB5-8E32-54A05EA49C31";
		public const string Email = "D785E3C0-AEF6-41FF-BE6F-B6ABEA630D5B";
		/// <summary>
		/// The user identifier
		/// </summary>
		public static int userId = 0;

		/// <summary>
		/// The car identifier
		/// </summary>
		public static int carId = 0;

		/// <summary>
		/// The kilometers
		/// </summary>
		public static int kilometers = 0;

		/// <summary>
		/// The country name
		/// </summary>
		public static string countryName = string.Empty;

		/// <summary>
		/// The admin area
		/// </summary>
		public static string adminArea = string.Empty;

		/// <summary>
		/// The locality
		/// </summary>
		public static string locality = string.Empty;

		/// <summary>
		/// The address
		/// </summary>
		public static string address = string.Empty;

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
		public static Placemark placemark = null;

		/// <summary>
		/// The URL
		/// </summary>
		public static Uri url;
	}
}