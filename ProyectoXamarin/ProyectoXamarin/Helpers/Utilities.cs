// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-13-2022
// ***********************************************************************
// <copyright file="Utilities.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Enums;
using ProyectoXamarin.Helpers;
using ProyectoXamarin.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Utilities))]

namespace ProyectoXamarin.Helpers
{
	/// <summary>
	/// Class Utilities.
	/// Implements the <see cref="IUtilities" />
	/// </summary>
	/// <seealso cref="IUtilities" />
	public class Utilities : IUtilities
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Utilities"/> class.
		/// </summary>
		public Utilities()
		{
		}

		/// <summary>
		/// Translates the specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns><see cref="System.String"/>.</returns>
		public string Translate(string data)
		{
			switch (data)
			{
				case nameof(EntityEnums.Location):
					data = "Localización";
					break;

				case nameof(EntityEnums.Car):
					data = "Coche";
					break;

				case nameof(EntityEnums.Kilometers):
					data = "Kilometros";
					break;

				case nameof(EntityEnums.User):
					data = "Usuario";
					break;

				default:
					break;
			}
			return data;
		}

		/// <summary>
		/// Gets the status.
		/// </summary>
		/// <param name="state">The state.</param>
		/// <param name="item">The item.</param>
		/// <returns>Task<see cref="Task" />.</returns>
		public async Task GetStatus(int state, string item)
		{
			item = Translate(item);

			switch (state)
			{
				case (int) StatusEnum.ErrorInProcess:
					await UserDialogs.Instance.AlertAsync($"Ha ocurrido un error en este proceso", "Error", "OK");
					break;

				case (int) StatusEnum.Registered:
					await UserDialogs.Instance.AlertAsync($"El {item} se ha registrado correctamente", "Registro", "OK");
					break;

				case (int) StatusEnum.Updated:
					await UserDialogs.Instance.AlertAsync($"El {item} se ha actualizado correctamente", "Actualización", "OK");
					break;

				case (int) StatusEnum.Deleted:
					await UserDialogs.Instance.AlertAsync($"El {item} se ha eliminado correctamente", "Eliminación", "OK");
					break;

				default:
					break;
			}
		}
	}
}