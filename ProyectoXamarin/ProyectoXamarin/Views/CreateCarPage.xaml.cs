// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-10-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="CreateCarPage.xaml.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ProyectoXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
	/// <summary>
	/// Class CreateCarPage.
	/// Implements the <see cref="ContentPage" />
	/// </summary>
	/// <seealso cref="ContentPage" />
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateCarPage : ContentPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CreateCarPage"/> class.
		/// </summary>
		public CreateCarPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			if (BindingContext is CreateCarViewModel createCarViewModel)
			{
				await createCarViewModel.OnAppearingAsync();
			}
		}
	}
}