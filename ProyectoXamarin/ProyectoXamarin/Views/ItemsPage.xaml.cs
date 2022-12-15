// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-23-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="ItemsPage.xaml.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ProyectoXamarin.ViewModels;
using Xamarin.Forms;

namespace ProyectoXamarin.Views
{
	/// <summary>
	/// Class ItemsPage.
	/// Implements the <see cref="ContentPage" />
	/// </summary>
	/// <seealso cref="ContentPage" />
	public partial class ItemsPage : ContentPage
	{
		/// <summary>
		/// The view model
		/// </summary>
		private ItemsViewModel _viewModel;

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemsPage"/> class.
		/// </summary>
		public ItemsPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new ItemsViewModel();
		}

		/// <summary>
		/// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAppearing();
		}
	}
}