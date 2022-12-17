// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-14-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-15-2022
// ***********************************************************************
// <copyright file="EmailService.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Services;
using RestSharp;
using RestSharp.Authenticators;
using Xamarin.Forms;

[assembly: Dependency(typeof(EmailService))]
namespace ProyectoXamarin.Services
{
	/// <summary>
	/// Class EmailService.
	/// Implements the <see cref="IEmailService" />
	/// </summary>
	/// <seealso cref="IEmailService" />
	public class EmailService : IEmailService
	{
		/// <summary>
		/// The API key
		/// </summary>
		private static string APIKey = "4a8f300080b031e5bc398e393f6d96f3-48d7d97c-aad54df5";

		/// <summary>
		/// The base URI
		/// </summary>
		private const string BaseUri = "https://api.mailgun.net/v3";

		/// <summary>
		/// The domain
		/// </summary>
		private const string Domain = "sandbox080a7f0eebe343949433148114a81913.mailgun.org";

		/// <summary>
		/// The sender address
		/// </summary>
		private const string SenderAddress = "<postmaster@sandbox080a7f0eebe343949433148114a81913.mailgun.org>";



		/// <summary>
		/// Initializes a new instance of the <see cref="EmailService" /> class.
		/// </summary>
		public EmailService()
		{
		}

		/// <summary>
		/// Send email as an asynchronous operation.
		/// </summary>
		/// <param name="toAddress">To address.</param>
		/// <param name="name">The name.</param>
		/// <param name="password">The password.</param>
		/// <returns>A Task<see cref="Task{RestSharp.RestResponse}" /> representing the asynchronous operation.</returns>
		public async Task<RestResponse> SendEmailAsync(string toAddress, string name, string password)
		{
			RestClient client = new RestClient(BaseUri);
			client.Authenticator = new HttpBasicAuthenticator("api", APIKey);
			RestRequest request = new RestRequest();
			request.AddParameter("domain", Domain, ParameterType.UrlSegment);
			request.Resource = "{domain}/messages";
			request.AddParameter("from", "FindCar, <muchabasura213@gmail.com>");
			request.AddParameter("to", $"{name} <{toAddress}>");
			request.AddParameter("subject", $"Hello {name}");
			request.AddParameter("text", $"Hola {name}, Hemos recibido una solicitud de restablecimiento de contraseña para la cuenta asociada a esta dirección de correo electrónico" +
					$"\nSu password es: {password}");
			request.Method = Method.Post;

			return await client.ExecuteAsync(request);

			
			//return client.Execute(request);
		}
	}
}