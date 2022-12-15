// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-14-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="IEmailService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using RestSharp;
using System.Threading.Tasks;

namespace ProyectoXamarin.Interfaces
{
	/// <summary>
	/// Interface IEmailService
	/// </summary>
	public interface IEmailService
	{
		/// <summary>
		/// Sends the email asynchronous.
		/// </summary>
		/// <param name="toAddress">To address.</param>
		/// <param name="name">The name.</param>
		/// <param name="password">The password.</param>
		/// <returns>Task<see cref="Task{RestSharp.RestResponse}" />.</returns>
		Task<RestResponse> SendEmailAsync(string toAddress, string name, string password);
	}
}
