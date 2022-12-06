﻿using Acr.UserDialogs;
using ProyectoXamarin.Enums;
using System;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Helpers;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Dependency(typeof(Utilities))]
namespace ProyectoXamarin.Helpers
{
	public class Utilities : IUtilities
	{
		public Utilities()
		{

		}
		public async Task GetSatatus(int state, string toast)
		{
			switch (state)
			{
				case (int) StatusEnum.NotRegistered:
					UserDialogs.Instance.Toast($"{toast} no registrado, intente de nuevo", TimeSpan.FromSeconds(2));
					break;
				case (int) StatusEnum.Registered:
					await UserDialogs.Instance.ConfirmAsync("Se ha registrado correctamente", "Registro", "OK");
					break;
				case (int) StatusEnum.Updated:
					await UserDialogs.Instance.ConfirmAsync("Se ha actualizado correctamente", "Actualización", "OK");
					break;
				default:
					break;
			}
		}
	}
}
