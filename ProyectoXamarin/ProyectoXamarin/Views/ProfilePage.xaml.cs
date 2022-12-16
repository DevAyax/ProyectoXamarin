// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-15-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-16-2022
// ***********************************************************************
// <copyright file="ProfilePage.xaml.cs" company="ProyectoXamarin">
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
	/// Class ProfilePage.
	/// Implements the <see cref="ContentPage" />
	/// </summary>
	/// <seealso cref="ContentPage" />
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProfilePage"/> class.
		/// </summary>
		public ProfilePage()
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
			if (BindingContext is ProfileViewModel profileViewModel)
			{
				await profileViewModel.OnAppearingAsync();
			}
		}
	}
}