// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="LoginPage.xaml.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ProyectoXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Xaml.Diagnostics;

namespace ProyectoXamarin.Views
{
	/// <summary>
	/// Class LoginPage.
	/// Implements the <see cref="ContentPage" />
	/// </summary>
	/// <seealso cref="ContentPage" />
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LoginPage"/> class.
		/// </summary>
		public LoginPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			if(BindingContext is LoginViewModel loginViewModel)
			{
				await loginViewModel.OnAppearing();
			}
		}
	}
}